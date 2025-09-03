@echo off
setlocal enabledelayedexpansion

:: 配置项目路径（根据实际情况修改）
set "CORE_PROJECT=./Jasper.Allinpay.Core/Jasper.Allinpay.Core.csproj"
set "ASPNET_PROJECT=./Jasper.Allinpay.AspNetCore/Jasper.Allinpay.AspNetCore.csproj"
set "OUTPUT_DIR=./nupkg"
set "NUGET_SOURCE=https://api.nuget.org/v3/index.json"

:: 检查环境变量 NugetKey 是否存在
if "!NugetKey!"=="" (
    echo 错误：未找到环境变量 "NugetKey"，请先设置 NuGet API 密钥
    echo 设置方法：setx NugetKey "你的API密钥"
    exit /b 1
)

:: 创建输出目录
if not exist "!OUTPUT_DIR!" (
    mkdir "!OUTPUT_DIR!"
    if !errorlevel! neq 0 (
        echo 错误：创建输出目录失败
        exit /b 1
    )
)

:: 清理项目
echo.
echo === 开始清理项目 ===
dotnet clean "!CORE_PROJECT!" --configuration Release
if !errorlevel! neq 0 (
    echo 错误：核心项目清理失败
    exit /b 1
)

dotnet clean "!ASPNET_PROJECT!" --configuration Release
if !errorlevel! neq 0 (
    echo 错误：AspNetCore项目清理失败
    exit /b 1
)

:: 构建项目
echo.
echo === 开始构建项目 ===
dotnet build "!CORE_PROJECT!" --configuration Release
if !errorlevel! neq 0 (
    echo 错误：核心项目构建失败
    exit /b 1
)

dotnet build "!ASPNET_PROJECT!" --configuration Release
if !errorlevel! neq 0 (
    echo 错误：AspNetCore项目构建失败
    exit /b 1
)

:: 生成NuGet包
echo.
echo === 开始生成NuGet包 ===
dotnet pack "!CORE_PROJECT!" --configuration Release --output "!OUTPUT_DIR!"
if !errorlevel! neq 0 (
    echo 错误：核心项目打包失败
    exit /b 1
)

dotnet pack "!ASPNET_PROJECT!" --configuration Release --output "!OUTPUT_DIR!"
if !errorlevel! neq 0 (
    echo 错误：AspNetCore项目打包失败
    exit /b 1
)

:: 查找生成的包文件
set "CORE_PACKAGE="
set "ASPNET_PACKAGE="

for /f "delims=" %%f in ('dir /b /od "!OUTPUT_DIR!\Jasper.Allinpay.Core.*.nupkg"') do set "CORE_PACKAGE=%%f"
for /f "delims=" %%f in ('dir /b /od "!OUTPUT_DIR!\Jasper.Allinpay.AspNetCore.*.nupkg"') do set "ASPNET_PACKAGE=%%f"

if "!CORE_PACKAGE!"=="" (
    echo 错误：未找到核心包
    exit /b 1
)

if "!ASPNET_PACKAGE!"=="" (
    echo 错误：未找到AspNetCore包
    exit /b 1
)

:: 发布到NuGet
echo.
echo === 开始发布核心包 ===
dotnet nuget push "!OUTPUT_DIR!\!CORE_PACKAGE!" --api-key "!NugetKey!" --source "!NUGET_SOURCE!"
if !errorlevel! neq 0 (
    echo 错误：核心包发布失败
    exit /b 1
)

echo.
echo === 开始发布AspNetCore包 ===
dotnet nuget push "!OUTPUT_DIR!\!ASPNET_PACKAGE!" --api-key "!NugetKey!" --source "!NUGET_SOURCE!"
if !errorlevel! neq 0 (
    echo 错误：AspNetCore包发布失败
    exit /b 1
)

echo.
echo === 所有操作完成，发布成功 ===
endlocal
