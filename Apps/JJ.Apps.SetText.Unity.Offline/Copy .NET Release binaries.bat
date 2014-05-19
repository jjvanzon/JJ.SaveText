@echo off
prompt $G
@echo on
rmdir /S /Q Assets\DotNetAssemblies 
mkdir Assets\DotNetAssemblies
xcopy ..\JJ.Apps.SetText.Unity.DotNet\bin\Release\*.* Assets\DotNetAssemblies /E /Q

REM  Delete dll's that indirectly reference System.Net.Sockets, 
REM  which is not supported by Unity Free if you want to deploy.

del Assets\DotNetAssemblies\JJ.Apps.SetText.AppService.Interface.dll
del Assets\DotNetAssemblies\JJ.Apps.SetText.Unity.DotNet.dll

del Assets\DotNetAssemblies\JJ.Apps.SetText.AppService.Interface.pdb
del Assets\DotNetAssemblies\JJ.Apps.SetText.Unity.DotNet.pdb

REM  TODO FOR WINDOWS PHONE: CLEAR THE ASSETS\MONO FOLDER!!!
REM  TODO FOR WINDOWS PHONE: COPY SATELLITE ASSEMBLIES TO WINDOWS PHONE DEPLOYMENT FOLDER!!!

pause
