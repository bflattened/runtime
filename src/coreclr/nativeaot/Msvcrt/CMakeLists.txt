project(msvcrt)

add_custom_command(
  OUTPUT msvcrt.lib
  COMMAND lib.exe /machine:X64 /def:${CMAKE_CURRENT_SOURCE_DIR}/msvcrt.def /out:msvcrt.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/msvcrt.def
  VERBATIM)

add_custom_target(msvcrtlib ALL
    DEPENDS msvcrt.lib
    VERBATIM)

add_dependencies(nativeaot msvcrtlib)

install (FILES ${CMAKE_CURRENT_BINARY_DIR}/msvcrt.lib DESTINATION aotsdk COMPONENT nativeaot)
