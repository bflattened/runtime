project(msvcrt)

if (CLR_CMAKE_HOST_ARCH_ARM64)
  set(LIB_MACHINE_FLAG arm64)
else()
  set(LIB_MACHINE_FLAG x64)
endif()

add_custom_command(
  OUTPUT api-ms-win-crt-conio-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-conio-l1-1-0.def /out:api-ms-win-crt-conio-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-conio-l1-1-0.def
  VERBATIM)
add_custom_target(coniolib ALL
    DEPENDS api-ms-win-crt-conio-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot coniolib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-conio-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-convert-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-convert-l1-1-0.def /out:api-ms-win-crt-convert-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-convert-l1-1-0.def
  VERBATIM)
add_custom_target(convertlib ALL
    DEPENDS api-ms-win-crt-convert-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot convertlib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-convert-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-environment-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-environment-l1-1-0.def /out:api-ms-win-crt-environment-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-environment-l1-1-0.def
  VERBATIM)
add_custom_target(environmentlib ALL
    DEPENDS api-ms-win-crt-environment-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot environmentlib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-environment-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-filesystem-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-filesystem-l1-1-0.def /out:api-ms-win-crt-filesystem-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-filesystem-l1-1-0.def
  VERBATIM)
add_custom_target(filesystemlib ALL
    DEPENDS api-ms-win-crt-filesystem-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot filesystemlib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-filesystem-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-heap-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-heap-l1-1-0.def /out:api-ms-win-crt-heap-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-heap-l1-1-0.def
  VERBATIM)
add_custom_target(heaplib ALL
    DEPENDS api-ms-win-crt-heap-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot heaplib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-heap-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-locale-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-locale-l1-1-0.def /out:api-ms-win-crt-locale-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-locale-l1-1-0.def
  VERBATIM)
add_custom_target(localelib ALL
    DEPENDS api-ms-win-crt-locale-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot localelib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-locale-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-multibyte-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-multibyte-l1-1-0.def /out:api-ms-win-crt-multibyte-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-multibyte-l1-1-0.def
  VERBATIM)
add_custom_target(multibytelib ALL
    DEPENDS api-ms-win-crt-multibyte-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot multibytelib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-multibyte-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-process-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-process-l1-1-0.def /out:api-ms-win-crt-process-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-process-l1-1-0.def
  VERBATIM)
add_custom_target(processlib ALL
    DEPENDS api-ms-win-crt-process-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot processlib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-process-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-runtime-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-runtime-l1-1-0.def /out:api-ms-win-crt-runtime-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-runtime-l1-1-0.def
  VERBATIM)
add_custom_target(runtimelib ALL
    DEPENDS api-ms-win-crt-runtime-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot runtimelib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-runtime-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-stdio-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-stdio-l1-1-0.def /out:api-ms-win-crt-stdio-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-stdio-l1-1-0.def
  VERBATIM)
add_custom_target(stdiolib ALL
    DEPENDS api-ms-win-crt-stdio-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot stdiolib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-stdio-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-string-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-string-l1-1-0.def /out:api-ms-win-crt-string-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-string-l1-1-0.def
  VERBATIM)
add_custom_target(stringlib ALL
    DEPENDS api-ms-win-crt-string-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot stringlib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-string-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-time-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-time-l1-1-0.def /out:api-ms-win-crt-time-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-time-l1-1-0.def
  VERBATIM)
add_custom_target(timelib ALL
    DEPENDS api-ms-win-crt-time-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot timelib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-time-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-utility-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-utility-l1-1-0.def /out:api-ms-win-crt-utility-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/api-ms-win-crt-utility-l1-1-0.def
  VERBATIM)
add_custom_target(utilitylib ALL
    DEPENDS api-ms-win-crt-utility-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot utilitylib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-utility-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)

add_custom_command(
  OUTPUT api-ms-win-crt-math-l1-1-0.lib
  COMMAND lib.exe /machine:${LIB_MACHINE_FLAG} /def:${CMAKE_CURRENT_SOURCE_DIR}/${LIB_MACHINE_FLAG}/api-ms-win-crt-math-l1-1-0.def /out:api-ms-win-crt-math-l1-1-0.lib
  DEPENDS ${CMAKE_CURRENT_SOURCE_DIR}/${LIB_MACHINE_FLAG}/api-ms-win-crt-math-l1-1-0.def
  VERBATIM)
add_custom_target(mathlib ALL
    DEPENDS api-ms-win-crt-math-l1-1-0.lib
    VERBATIM)
add_dependencies(nativeaot mathlib)
install (FILES ${CMAKE_CURRENT_BINARY_DIR}/api-ms-win-crt-math-l1-1-0.lib DESTINATION aotsdk COMPONENT nativeaot)
