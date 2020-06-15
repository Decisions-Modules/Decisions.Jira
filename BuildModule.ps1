#R equires -RunAsAdministrator

param (
    [Parameter(Mandatory=$false)][string]$msbuild,
    [Parameter(Mandatory=$false)][string]$framework
)

function FindMSBuild {
    $guess = Join-Path -Path ${Env:ProgramFiles(x86)} -ChildPath "Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe"
    if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }

    # Check for Visual Studio MSBuild version 2019 (15.0)
    $guess = Join-Path -Path ${Env:ProgramFiles(x86)} -ChildPath "Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\bin\msbuild.exe"
    if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }

    # Check for Visual Studio MSBuild version 2019 (15.0)
    $guess = Join-Path -Path ${Env:ProgramFiles(x86)} -ChildPath "Microsoft Visual Studio\2019\Professional\MSBuild\Current\bin\msbuild.exe"
        if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }

    $guess = Join-Path -Path ${Env:ProgramFiles(x86)} -ChildPath "\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe"
    if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }

    $guess = Join-Path -Path ${Env:ProgramFiles(x86)} -ChildPath "\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\bin\msbuild.exe"
    if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }

    $guess = Join-Path -Path ${Env:ProgramFiles(x86)} -ChildPath "\Microsoft Visual Studio\2019\Professional\MSBuild\Current\bin\msbuild.exe"
    if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }

    $guess = Join-Path -Path ${Env:ProgramFiles(x86)} -ChildPath "\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\bin\msbuild.exe"
    if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }

    $guess = Join-Path -Path ${Env:ProgramFiles(x86)} -ChildPath "\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\msbuild.exe"
    if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }
}

function FindFrameworkFiles {
    #search sub folders for nuget acquired framework files
    #location should include DecisionsFramework.dll DecisionsFramework.NET.dll and CoreSErviceClients.dll

    try {
        $firstRes = (Get-ChildItem -Path . -Include "DecisionsFramework.Net.dll" -Recurse -ErrorAction SilentlyContinue | Where-Object { ($_.PSIsContainer -eq $false) -and  ( $_.Name -like "*$fileName*") })[0].DirectoryName
    } catch {}
    if (!$firstRes)
    {
        $firstRes = (Get-ChildItem -Path "c:\Program Files\Decisions\Decisions Services Manager" -Include "DecisionsFramework.Net.dll" -Recurse -ErrorAction SilentlyContinue | Where-Object { ($_.PSIsContainer -eq $false) -and  ( $_.Name -like "*$fileName*") })[0].DirectoryName
    }
    return $firstRes
}

function ModifyModuleBuildFile($frameworkPath, $basePath) {
    # Assuming the most common module build file
    $buildfile = Join-Path -Path $basePath -ChildPath "Module.Build.xml"
    Write-Output "Loading Module.xml from $buildfile"
    [xml]$doc = Get-Content $buildfile
    $doc.PreserveWhitespace = $true
    $doc.CreateModuleInfo.Clients.DotNetCoreDll = "$frameworkPath\CoreServicesClients.Net.dll"
    $doc.CreateModuleInfo.Clients.DotNetFrontendFrameworkDll = "$frameworkPath\DecisionsFramework.Net.dll"

    $resultFile = Join-Path -Path $basePath -ChildPath "Module.Modified.xml"
    $doc.Save($resultFile)
}

function GetCompileTarget($basePath) {
    $guess = Join-Path -Path $basePath -ChildPath "build.proj"
    if (Test-Path -PathType leaf -LiteralPath $guess ) {
        return $guess
    }
    throw "Could not find a build.proj file, please create one."
}


function StopHostManager {
    $local:service = (get-service "servicehostmanager");
    $local:serviceWatcher = (get-service "servicehostmanagerwatcher")

    if ($local:serviceWatcher.status -eq "Running") {
        $local:serviceWatcher.Stop()
    }
    if ($local:service.status -eq "Running") {
        $local:service.Stop()
    }

    Write-Output "stopping SHM..."
    do { $local:service.refresh(); sleep 1; } until ($local:service.status -eq "Stopped")
}

function StartHostManager {
    $local:service = (get-service "servicehostmanager");
    Write-Output "starting SHM..."
    $local:service.Start()

    do { $local:service.refresh(); sleep 1; } until ($local:service.status -eq "Running")
    Write-Output "SHM Started."
}

function FindModuleName($buildProj)
{
    [xml]$local:XmlDocument = Get-Content -Path $buildProj

    foreach($local:target in $local:XmlDocument.Project.Target){
        if($local:target.Name -eq "build_module")
        {
            $local:cmdline = $local:target.Exec.Command
            break
        }
    }

    $local:cmdline = $local:cmdline -split ' '
    $local:index = [array]::indexof($local:cmdline,"-buildmodule")
    $local:index++

    return $local:cmdline[$local:index]
}

function CopyModule($basePath)
{
    $local:moduleName = FindModuleName("$basePath\build.proj")
    $local:fullModuleName = "$basePath\$local:moduleName.zip"
    $local:destination  = "C:\Program Files\Decisions\Decisions Services Manager\CustomModules\$local:moduleName.zip"

    Copy-Item $local:fullModuleName $local:destination
}

function FindSolutionFile($basePath) {
    $local:filelist = Get-ChildItem -Path "$basePath\*.sln"
    if($local:filelist.Length -eq 0)
    {
        throw "Can not find *.sln file"
    }
    return $local:filelist[0].FullName
}

if ($msbuild) {
    Write-Output "Using $msbuild"
} else {
    Write-Output "Looking for MSBUILD.exe"
    $msbuild = FindMSBuild
    Write-Output "Found and Trying $msbuild"
}

if (!$framework) {
    Write-Output "Attempting to determine and adjust paths, and build module."
    $framework = FindFrameworkFiles
    if (!$framework) {
        Write-Output "We cannot find any instance of the sdk by searching for DecisionsFramework, DecisionsFramework.NET and CoreServiceClients.  Please run again and use the -framework flag."
        exit
    }
    Write-Output "Using framework found at: $framework"
} else {
    Write-Output "Using specified framework files location: $framework"
    if (!(Test-Path -LiteralPath $framework)) {
        Write-Output "Checking your path for the -framework flag and could not find this path."
        exit
    }
}

Write-Output "Modifying Module.Build.xml"
$basePath = (Get-Location).Path
Write-Output "Using basePath = $basePath"
ModifyModuleBuildFile $framework $basePath
Write-Output "Module.Modified.xml should now exist with correct paths.  Please check if not found."

Write-Output "Compiling Project by build.proj, or by .sln file."
$compiletarget = GetCompileTarget $basepath

$solution = FindSolutionFile($basePath)
& $msbuild -t:restore $solution
& $msbuild $compiletarget
if ($LastExitCode -ne 0)
{
   throw "Compile failed with the return code: $LastExitCode"
}

StopHostManager
CopyModule($basePath)
StartHostManager

