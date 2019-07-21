@echo off
set OutName=SmartTask
taskkill -f -im %OutName%.exe
type %~dp0\Banner.txt
@REM 需要配置本地环境变量 VS150COMNTOOLS 到vs的开发人员命令行目录
call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\VsDevCmd.bat"
MSBuild %~dp0\SmartTask.sln /t:Rebuild /p:Configuration=Release;AssemblyName=%OutName%
echo Release Success
@REM 生成成功后启动程序
%~dp0\release\%OutName%
pause