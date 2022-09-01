#if defined(_WIN32)
#include <windows.h>
extern "C" void InitCRTEnvironment(void);
extern "C" void ExitCRTEnvironment(void);
extern "C" BOOL WINAPI _DllMainCRTStartup(
    HINSTANCE const instance,
    DWORD     const reason,
    LPVOID    const reserved
    )
{
    if (reason == DLL_PROCESS_ATTACH)
    {
        InitCRTEnvironment();
    }
    if (reason == DLL_PROCESS_DETACH)
    {
        ExitCRTEnvironment();
    }
    return TRUE;
}
#endif
