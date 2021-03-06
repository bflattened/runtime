
add_library(monoapi_utils INTERFACE)

set(utils_public_headers_base
    mono-logger.h
    mono-error.h
    mono-forward.h
    mono-publib.h
    mono-jemalloc.h
    mono-dl-fallback.h
    mono-private-unstable.h
    mono-counters.h
    )
set(utils_public_headers_details_base
    details/mono-publib-types.h
    details/mono-publib-functions.h
    details/mono-logger-types.h
    details/mono-logger-functions.h
    details/mono-error-types.h
    details/mono-error-functions.h
    details/mono-dl-fallback-types.h
    details/mono-dl-fallback-functions.h
    details/mono-counters-types.h
    details/mono-counters-functions.h
    )
addprefix(utils_public_headers mono/utils "${utils_public_headers_base}")
addprefix(utils_public_headers_details mono/utils "${utils_public_headers_details_base}")

target_sources(monoapi_utils INTERFACE ${utils_public_headers} ${utils_public_headers_details})

target_include_directories(monoapi_utils INTERFACE .)

add_library(monoapi_metadata INTERFACE)

set(metadata_public_headers_base
	appdomain.h
	assembly.h
	attrdefs.h
	blob.h
	class.h
	debug-helpers.h
	debug-mono-symfile.h
	environment.h
	exception.h
	image.h
	loader.h
	metadata.h
	mono-config.h
	mono-debug.h
	mono-gc.h
	mono-private-unstable.h
	object.h
	object-forward.h
	opcodes.h
	profiler.h
	profiler-events.h
	reflection.h
	row-indexes.h
	sgen-bridge.h
	threads.h
	tokentype.h
	verify.h
	)
set(metadata_public_headers_details_base
	details/environment-functions.h
	details/opcodes-types.h
	details/opcodes-functions.h
	details/sgen-bridge-types.h
	details/sgen-bridge-functions.h
	details/image-types.h
	details/image-functions.h
	details/metadata-types.h
	details/metadata-functions.h
	details/assembly-types.h
	details/assembly-functions.h
	details/loader-types.h
	details/loader-functions.h
	details/class-types.h
	details/class-functions.h
	details/object-types.h
	details/object-functions.h
	details/exception-types.h
	details/exception-functions.h
	details/reflection-types.h
	details/reflection-functions.h
	details/appdomain-types.h
	details/appdomain-functions.h
	details/threads-types.h
	details/threads-functions.h
	details/debug-helpers-types.h
	details/debug-helpers-functions.h
	details/mono-debug-types.h
	details/mono-debug-functions.h
	details/mono-gc-types.h
	details/mono-gc-functions.h
	details/mono-config-types.h
	details/mono-config-functions.h
	details/profiler-types.h
	details/profiler-functions.h
	details/mono-private-unstable-types.h
	details/mono-private-unstable-functions.h
	)
addprefix(metadata_public_headers mono/metadata "${metadata_public_headers_base}")
addprefix(metadata_public_headers_details mono/metadata "${metadata_public_headers_details_base}")

target_sources(monoapi_metadata INTERFACE ${metadata_public_headers} ${metadata_public_headers_details})

target_include_directories(monoapi_metadata INTERFACE .)

add_library(monoapi_jit INTERFACE)

set(jit_public_headers_base
  jit.h
  mono-private-unstable.h
  )
set(jit_public_headers_details_base
  details/jit-types.h
  details/jit-functions.h
  details/mono-private-unstable-types.h
  details/mono-private-unstable-functions.h
  )
addprefix(jit_public_headers mono/jit "${jit_public_headers_base}")
addprefix(jit_public_headers_details mono/jit "${jit_public_headers_details_base}")

target_sources(monoapi_jit INTERFACE ${jit_public_headers} ${jit_public_headers_details})

target_include_directories(monoapi_jit INTERFACE .)


add_library(monoapi INTERFACE)
target_link_libraries(monoapi INTERFACE monoapi_utils monoapi_metadata monoapi_jit)

if(INSTALL_MONO_API)
  install(FILES ${utils_public_headers} DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}/mono-2.0/mono/utils)
  install(FILES ${utils_public_headers_details} DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}/mono-2.0/mono/utils/details)
  install(FILES ${metadata_public_headers} DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}/mono-2.0/mono/metadata)
  install(FILES ${metadata_public_headers_details} DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}/mono-2.0/mono/metadata/details)
  install(FILES ${jit_public_headers} DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}/mono-2.0/mono/jit)
  install(FILES ${jit_public_headers_details} DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}/mono-2.0/mono/jit/details)
endif()
