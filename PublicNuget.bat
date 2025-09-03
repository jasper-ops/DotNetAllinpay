@echo off
setlocal enabledelayedexpansion

:: ������Ŀ·��������ʵ������޸ģ�
set "CORE_PROJECT=./Jasper.Allinpay.Core/Jasper.Allinpay.Core.csproj"
set "ASPNET_PROJECT=./Jasper.Allinpay.AspNetCore/Jasper.Allinpay.AspNetCore.csproj"
set "OUTPUT_DIR=./nupkg"
set "NUGET_SOURCE=https://api.nuget.org/v3/index.json"

:: ��黷������ NugetKey �Ƿ����
if "!NugetKey!"=="" (
    echo ����δ�ҵ��������� "NugetKey"���������� NuGet API ��Կ
    echo ���÷�����setx NugetKey "���API��Կ"
    exit /b 1
)

:: �������Ŀ¼
if not exist "!OUTPUT_DIR!" (
    mkdir "!OUTPUT_DIR!"
    if !errorlevel! neq 0 (
        echo ���󣺴������Ŀ¼ʧ��
        exit /b 1
    )
)

:: ������Ŀ
echo.
echo === ��ʼ������Ŀ ===
dotnet clean "!CORE_PROJECT!" --configuration Release
if !errorlevel! neq 0 (
    echo ���󣺺�����Ŀ����ʧ��
    exit /b 1
)

dotnet clean "!ASPNET_PROJECT!" --configuration Release
if !errorlevel! neq 0 (
    echo ����AspNetCore��Ŀ����ʧ��
    exit /b 1
)

:: ������Ŀ
echo.
echo === ��ʼ������Ŀ ===
dotnet build "!CORE_PROJECT!" --configuration Release
if !errorlevel! neq 0 (
    echo ���󣺺�����Ŀ����ʧ��
    exit /b 1
)

dotnet build "!ASPNET_PROJECT!" --configuration Release
if !errorlevel! neq 0 (
    echo ����AspNetCore��Ŀ����ʧ��
    exit /b 1
)

:: ����NuGet��
echo.
echo === ��ʼ����NuGet�� ===
dotnet pack "!CORE_PROJECT!" --configuration Release --output "!OUTPUT_DIR!"
if !errorlevel! neq 0 (
    echo ���󣺺�����Ŀ���ʧ��
    exit /b 1
)

dotnet pack "!ASPNET_PROJECT!" --configuration Release --output "!OUTPUT_DIR!"
if !errorlevel! neq 0 (
    echo ����AspNetCore��Ŀ���ʧ��
    exit /b 1
)

:: �������ɵİ��ļ�
set "CORE_PACKAGE="
set "ASPNET_PACKAGE="

for /f "delims=" %%f in ('dir /b /od "!OUTPUT_DIR!\Jasper.Allinpay.Core.*.nupkg"') do set "CORE_PACKAGE=%%f"
for /f "delims=" %%f in ('dir /b /od "!OUTPUT_DIR!\Jasper.Allinpay.AspNetCore.*.nupkg"') do set "ASPNET_PACKAGE=%%f"

if "!CORE_PACKAGE!"=="" (
    echo ����δ�ҵ����İ�
    exit /b 1
)

if "!ASPNET_PACKAGE!"=="" (
    echo ����δ�ҵ�AspNetCore��
    exit /b 1
)

:: ������NuGet
echo.
echo === ��ʼ�������İ� ===
dotnet nuget push "!OUTPUT_DIR!\!CORE_PACKAGE!" --api-key "!NugetKey!" --source "!NUGET_SOURCE!"
if !errorlevel! neq 0 (
    echo ���󣺺��İ�����ʧ��
    exit /b 1
)

echo.
echo === ��ʼ����AspNetCore�� ===
dotnet nuget push "!OUTPUT_DIR!\!ASPNET_PACKAGE!" --api-key "!NugetKey!" --source "!NUGET_SOURCE!"
if !errorlevel! neq 0 (
    echo ����AspNetCore������ʧ��
    exit /b 1
)

echo.
echo === ���в�����ɣ������ɹ� ===
endlocal
