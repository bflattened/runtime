#!/usr/bin/env bash

ScriptDir="$(cd "$(dirname "$0")"; pwd -P)"
ArtifactsDir="$1"
RepoRoot="$2"
BuildArch="$3"
TargetArch="$4"
BuildType="${5:-Release}"
CompilerId="$(echo "$6" | tr "[:upper:]" "[:lower:]")"

LLVMBranch="bflat-objwriter/release/7.0"

# Check that we have enough arguments
if [ $# -lt 4 ]; then
    echo "Usage: $(basename $0) ArtifactsDir RepoRoot BuildArch TargetArch [BuildType [CompilerId]]"
    exit 1
fi

if [ -z "$CompilerId" ]; then
    Compiler=clang
    CompilerMajorVer=
    CompilerMinorVer=
# Expecting a compiler id similar to -clang9 or -gcc10.2. See also eng/native/build-commons.sh.
elif [[ "$CompilerId" =~ ^-?([a-z]+)(-?([0-9]+)(\.([0-9]+))?)?$ ]]; then
    Compiler=${BASH_REMATCH[1]}
    CompilerMajorVer=${BASH_REMATCH[3]}
    CompilerMinorVer=${BASH_REMATCH[5]}
    if [[ "$Compiler" == "clang" && -n "$CompilerMajorVer" && "$CompilerMajorVer" -le 6
        && -z "$CompilerMinorVer" ]]; then
        CompilerMinorVer=0
    fi
else
    echo "Unexpected compiler identifier '$6'"
    exit 1
fi

cd "$ArtifactsDir" || exit 1

if [ ! -d llvm-project ]; then
    # Clone the LLVM repo
    git clone --depth 1 -b $LLVMBranch https://github.com/bflattened/llvm-project.git || exit 1
fi

cd llvm-project/llvm || exit 1

# Configure and build objwriter
mkdir -p "build/$TargetArch" || exit 1

if [ "$TargetArch" != "$BuildArch" ]; then
    export CROSSCOMPILE=1
fi

# Script arguments:
#   <path to top-level CMakeLists.txt> <path to intermediate directory> <architecture>
#   <compiler> <compiler major version> <compiler minor version>
#   [build flavor] [ninja] [scan-build] [cmakeargs]
"${RepoRoot}eng/native/gen-buildsys.sh" \
    "$PWD" "$PWD/build/$TargetArch" "$TargetArch" \
    "$Compiler" "$CompilerMajorVer" "$CompilerMinorVer" "$BuildType" \
    -DCMAKE_BUILD_TYPE="$BuildType" \
    -DCMAKE_INSTALL_PREFIX=install \
    -DLLVM_BUILD_TOOLS=0 \
    -DLLVM_ENABLE_TERMINFO=0 \
    -DLLVM_INCLUDE_UTILS=0 \
    -DLLVM_INCLUDE_RUNTIMES=0 \
    -DLLVM_INCLUDE_EXAMPLES=0 \
    -DLLVM_INCLUDE_TESTS=0 \
    -DLLVM_INCLUDE_DOCS=0 \
    -DLLVM_TARGETS_TO_BUILD="AArch64;ARM;X86" \
    || exit 1

# Get the number of available processors
Platform="$(uname)"
if [[ "$Platform" == "FreeBSD" ]]; then
    NumProc=$(sysctl -n hw.ncpu)
elif [[ "$Platform" == "NetBSD" || "$Platform" == "SunOS" ]]; then
    NumProc=$(getconf NPROCESSORS_ONLN)
elif [[ "$Platform" == "Darwin" ]]; then
    NumProc=$(getconf _NPROCESSORS_ONLN)
else
    NumProc=$(nproc --all)
fi

MaxJobs=$((NumProc+1))

echo "Executing cmake --build \"build/$TargetArch\" --config \"$BuildType\" --target objwriter -j \"$MaxJobs\""
cmake --build "build/$TargetArch" --config "$BuildType" --target objwriter -j "$MaxJobs" || exit 1
echo "Done building target objwriter"
