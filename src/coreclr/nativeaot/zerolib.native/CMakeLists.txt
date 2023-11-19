project(zerolibnative)

if (WIN32)
  set(ASM_SUFFIX asm)
else()
  set(ASM_SUFFIX S)
endif()

function(install_bootstrapper_object targetName destination)
  add_dependencies(nativeaot ${targetName})
  if (MSVC)
    install (FILES $<TARGET_OBJECTS:${targetName}> DESTINATION ${destination} COMPONENT nativeaot RENAME ${targetName}.obj)
  else()
    install (FILES $<TARGET_OBJECTS:${targetName}> DESTINATION ${destination} COMPONENT nativeaot RENAME lib${targetName}.o)
  endif()
endfunction()

if (CLR_CMAKE_HOST_ARCH_ARM64 OR CLR_CMAKE_HOST_ARCH_X86)
  set(SOURCES ${ARCH_SOURCES_DIR}/stubs.${ASM_SUFFIX})

  convert_to_absolute_path(SOURCES ${SOURCES})

  if (CLR_CMAKE_TARGET_WIN32)
    preprocess_files(SOURCES ${SOURCES})
  endif()

  add_library(zerolibnative OBJECT ${SOURCES})
  set_target_properties(zerolibnative PROPERTIES OUTPUT_NAME zerolibnative)

  install_bootstrapper_object(zerolibnative aotsdk)
endif()
