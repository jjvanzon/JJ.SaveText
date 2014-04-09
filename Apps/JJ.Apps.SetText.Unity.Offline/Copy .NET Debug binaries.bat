@echo off
prompt $G
@echo on
rmdir /S /Q Assets\Libraries 
mkdir Assets\Libraries
xcopy ..\JJ.Apps.SetText.Unity.DotNet\bin\Debug\*.* Assets\Libraries /E /Q
pause