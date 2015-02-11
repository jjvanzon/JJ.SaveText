@echo off
prompt $G
@echo on
rmdir /S /Q Assets\DotNetAssemblies
mkdir Assets\DotNetAssemblies
xcopy ..\JJ.Apps.SetText.Unity.DotNet.Online.WwwObject\bin\Debug\*.* Assets\DotNetAssemblies /E /Q

REM  TODO FOR WINDOWS PHONE: CLEAR THE ASSETS\MONO FOLDER!!!
REM  TODO FOR WINDOWS PHONE: COPY SATELLITE ASSEMBLIES TO WINDOWS PHONE DEPLOYMENT FOLDER!!!

pause
