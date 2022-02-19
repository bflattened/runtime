#if defined(_WIN32)
#include <windows.h>
extern "C" void InitCRTEnvironment(void);
extern "C" void ExitCRTEnvironment(void);
extern "C" int __cdecl wmain(int, LPWSTR *);
extern "C" int wmainCRTStartup()
{
    InitCRTEnvironment();
    int argc;
    LPWSTR* argv = CommandLineToArgvW(GetCommandLineW(), &argc);
    int exitcode = wmain(argc, argv);
    ExitCRTEnvironment();
    ExitProcess(exitcode);
    return 0;
}
#endif
