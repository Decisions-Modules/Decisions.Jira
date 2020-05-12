param (
	[Parameter(Mandatory=$false)][string]$msbuild,
	[Parameter(Mandatory=$false)][string]$framework
)

function FindMSBuild {
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
	$firstRes = (Get-ChildItem -Path . -Include "DecisionsFramework.Net.dll" -Recurse -ErrorAction SilentlyContinue | Where-Object { ($_.PSIsContainer -eq $false) -and  ( $_.Name -like "*$fileName*") }).DirectoryName
	if (!$firstRes)
	{
		$firstRes = (Get-ChildItem -Path "c:\Program Files\Decisions\Decisions Services Manager" -Include "DecisionsFramework.Net.dll" -Recurse -ErrorAction SilentlyContinue | Where-Object { ($_.PSIsContainer -eq $false) -and  ( $_.Name -like "*$fileName*") }).DirectoryName
	}
	return $firstRes
}

function ModifyModuleBuildFile($frameworkPath, $destinationPath) {

	# Assuming the most common module build file 
	$buildfile = "Module.Build.xml" 
	[xml]$doc = Get-Content $buildfile
	$doc.PreserveWhitespace = $true 
	$doc.CreateModuleInfo.Clients.DotNetCoreDll = "$frameworkPath\CoreServicesClients.Net.dll"
	$doc.CreateModuleInfo.Clients.DotNetFrontendFrameworkDll = "$frameworkPath\DecisionsFramework.Net.dll"
	
	$resultFile = Join-Path -Path $destinationPath -ChildPath "Module.Modified.xml"
	$doc.Save($resultFile)

}

function GetCompileTarget($basePath) {
	$guess = Join-Path -Path $basePath -ChildPath "build.proj"
	if (Test-Path -PathType leaf -LiteralPath $guess ) {
		return $guess
	}
	Write-Output "Could not find a build.proj file, please create one."
	exit
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
$basepath = Get-Location
ModifyModuleBuildFile $framework $basepath
Write-Output "Module.Modified.xml should now exist with correct paths.  Please check if not found."

Write-Output "Compiling Project by build.proj, or by .sln file."
$compiletarget = GetCompileTarget $basepath

Start-Process -Wait -FilePath "$msbuild" -Args "$compiletarget" -WorkingDirectory "." -RedirectStandardOutput "BuildModule.ps1.log" -RedirectStandardError "BuildModule.ps1.error" 