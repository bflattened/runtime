@echo off
setlocal

set "ScriptDir=%~dp0"
set "ArtifactsDir=%~1"
set "RepoRoot=%~2"
set "BuildArch=%~3"
set "TargetArch=%~4"
set "BuildType=%~5"
if "%BuildType%"=="" set "BuildType=Release"

set LLVMBranch=bflat-objwriter/release/7.0

:: Check that we have enough arguments
if "%TargetArch%"=="" (
    echo Usage: %~nx0 ArtifactsDir RepoRoot BuildArch TargetArch [BuildType]
    goto Error
)

cd "%ArtifactsDir%" || goto Error

if not exist llvm-project (
    rem Clone the LLVM repo
    git clone --depth 1 -b %LLVMBranch% https://github.com/bflattened/llvm-project.git || goto Error
)

cd llvm-project\llvm || goto Error

:: Init VS environment
call "%RepoRoot%eng\native\init-vs-env.cmd" || goto Error

:: Set CMakePath by evaluating the output from set-cmake-path.ps1
for /f "delims=" %%a in ('powershell -NoProfile -ExecutionPolicy ByPass "& ""%RepoRoot%eng\native\set-cmake-path.ps1"""') do %%a
echo Using CMake at "%CMakePath%"

:: Configure and build objwriter
if /i "%BuildArch%"=="%TargetArch%" (
    call :BuildLlvmTarget objwriter "%TargetArch%" || goto Error
    exit /b 0
)

rem Cross-build: first build llvm-tblgen.exe for the build architecture
set "TableGen=%CD%\build\%BuildArch%\%BuildType%\bin\llvm-tblgen.exe"

if not exist "%TableGen%" (
    echo Building llvm-tablegen.exe
    call :BuildLlvmTarget llvm-tblgen "%BuildArch%" || goto Error
    if not exist "%TableGen%" goto Error
)

rem Now use llvm-tblgen.exe to build objwriter for the target architecture
set "CMakeArch=%TargetArch%"
if /i "%TargetArch%"=="x86" set "CMakeArch=Win32"

call :BuildLlvmTarget objwriter "%TargetArch%" "-A %CMakeArch% -DLLVM_TABLEGEN=%TableGen%" || goto Error
exit /b 0

:BuildLlvmTarget
set "Target=%~1"
set "Arch=%~2"
set "ExtraCMakeArgs=%~3"

"%CMakePath%" -S . -B "build\%Arch%" ^
    %ExtraCMakeArgs% ^
    -DCMAKE_BUILD_TYPE=%BuildType% ^
    -DCMAKE_INSTALL_PREFIX=install ^
    -DLLVM_BUILD_TOOLS=0 ^
    -DLLVM_ENABLE_TERMINFO=0 ^
    -DLLVM_INCLUDE_UTILS=0 ^
    -DLLVM_INCLUDE_RUNTIMES=0 ^
    -DLLVM_INCLUDE_EXAMPLES=0 ^
    -DLLVM_INCLUDE_TESTS=0 ^
    -DLLVM_INCLUDE_DOCS=0 ^
    -DLLVM_TARGETS_TO_BUILD="AArch64;ARM;X86" ^
    -DLLVM_USE_CRT_DEBUG=MTd ^
    -DLLVM_USE_CRT_RELEASE=MT ^
    || goto Error

echo Executing "%CMakePath%" --build "build\%Arch%" --config %BuildType% --target %Target% -- -m
"%CMakePath%" --build "build\%Arch%" --config %BuildType% --target %Target% -- -m || goto Error
echo Done building target %Target%
exit /b 0

:Error

echo *** ERROR ***

exit /b 1
