project(Shcrt)

set(SOURCES
    wmain.cpp
    dllmain.cpp
    nocrt.cpp
)

set(ASM_SOURCES
    ${ARCH_SOURCES_DIR}/chkstk.asm
)

if (CLR_CMAKE_HOST_ARCH_ARM64)
  list(APPEND ASM_SOURCES
    ${ARCH_SOURCES_DIR}/interlocked.asm
  )
endif()

convert_to_absolute_path(ASM_SOURCES ${ASM_SOURCES})
preprocess_files(ASM_SOURCES ${ASM_SOURCES})

add_library(shcrt STATIC ${SOURCES} ${ASM_SOURCES})

install_static_library(shcrt aotsdk nativeaot)
