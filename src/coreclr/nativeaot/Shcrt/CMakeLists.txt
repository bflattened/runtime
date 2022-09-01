project(Shcrt)

set(SOURCES
    wmain.cpp
    dllmain.cpp
    nocrt.cpp
)

add_library(shcrt STATIC ${SOURCES})

install_static_library(shcrt aotsdk nativeaot)
