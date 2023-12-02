@echo off

for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -find vc\Auxiliary\Build\vcvarsall.bat`) do (
  "%%i" amd64_x86
  mkdir artifacts\bin\coreclr\windows.x86.Release\aotsdk
  ml /c /Foartifacts\bin\coreclr\windows.x86.Release\aotsdk\zerolibnative.obj /Zi src\coreclr\nativeaot\zerolib.native\x86\stubs.asm
  exit /b !errorlevel!
)