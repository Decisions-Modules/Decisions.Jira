@echo off
@echo Building with MsBuild
net stop "Service Host Manager Watcher"
net stop "Service Host Manager"
IF Exist "%programfiles(x86)%\MSBuild\14.0" (
	set MSBUILDVER=14.0
) else (
	set MSBUILDVER=12.0
)

echo Running: "%programfiles(x86)%\MSBuild\%MSBUILDVER%\bin\msbuild.exe"
"%programfiles(x86)%\MSBuild\%MSBUILDVER%\bin\msbuild.exe" build.proj
pause
copy ..\..\output\modules\Decisions.Jira.zip "c:\Program Files\Decisions\Decisions Services Manager\modules" /Y
net start "Service Host Manager"
