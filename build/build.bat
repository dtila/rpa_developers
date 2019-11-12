@echo off
echo Building solution ..
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\Tools\VsDevCmd.bat"

msbuild ..\src\Links.sln /t:Clean,Build /p:Configuration=Release

echo Packing ...
nuget pack project.nuspec -Properties Configuration=Release

pause