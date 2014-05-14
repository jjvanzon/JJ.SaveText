@echo off
prompt $G
@echo on
rmdir /S /Q Assets\Libraries 
mkdir Assets\Libraries
xcopy ..\JJ.Apps.SetText.Unity.DotNet\bin\Debug\*.* Assets\Libraries /E /Q

REM  Delete dll's that indirectly reference System.Net.Sockets, 
REM  which is not supported by Unity free if you want to deploy.

del Assets\Libraries\JJ.Apps.SetText.AppService.Interface.dll
del Assets\Libraries\JJ.Apps.SetText.Unity.DotNet.dll

del Assets\Libraries\JJ.Apps.SetText.AppService.Interface.pdb
del Assets\Libraries\JJ.Apps.SetText.Unity.DotNet.pdb

REM  TODO: COPY SATELLITE ASSEMBLIES TO WINDOWS PHONE DEPLOYMENT FOLDER!

pause

