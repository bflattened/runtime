#ifdef _WIN32
#define _CRT_DISABLE 1

extern "C" float nanf(char*)
{
    return __builtin_nanf("0");
}

extern "C" double nan(char*)
{
    return __builtin_nan("0");
}

extern "C" double round(double d)
{
    // Whatever...
    return (double)(__int64)(d + 0.5);
}

extern "C" float roundf(float d)
{
    // Whatever...
    return (float)(int)(d + 0.5f);
}


#define nan not_really_nan
#define nanf not_really_nanf
#define round not_really_round
#define roundf not_really_roundf


#include <new>
#include <cmath>
namespace std {
const std::nothrow_t nothrow = std::nothrow_t(); // define nothrow
}

#pragma function(_dclass)
extern "C" short _dclass(double x)
{
	union {double f; uint64_t i;} u = {x};
	int e = u.i>>52 & 0x7ff;
	if (!e) return u.i<<1 ? FP_SUBNORMAL : FP_ZERO;
	if (e==0x7ff) return u.i<<12 ? FP_NAN : FP_INFINITE;
	return FP_NORMAL;
}

#pragma function(_fdclass)
extern "C" short _fdclass(float x)
{
	union {float f; uint32_t i;} u = {x};
	int e = u.i>>23 & 0xff;
	if (!e) return u.i<<1 ? FP_SUBNORMAL : FP_ZERO;
	if (e==0xff) return u.i<<9 ? FP_NAN : FP_INFINITE;
	return FP_NORMAL;
}

void* operator new[](size_t const size, std::nothrow_t const& x) noexcept
{
    return malloc(size);
}

void* operator new(size_t const size, std::nothrow_t const& x) noexcept
{
    return malloc(size);
}

void operator delete[](void* const block) noexcept
{
    free(block);
}


void operator delete(void* const block, size_t const) noexcept
{
    free(block);
}

#if _CRT_DISABLE

extern "C" int _fltused = 0x9875;

#define WIN32_LEAN_AND_MEAN 
#include <stdint.h>
#include <limits.h>
#include <windows.h>

//#include <Windows.h>
#define ArrayCount(Name) (sizeof(Name)/(sizeof(Name[0]))
#define _CRTALLOC(x) __declspec(allocate(x))
#define nullptr 0

typedef int (__cdecl* _PIFV)(void);
typedef void (__cdecl* _PVFV)(void);

void __acrt_iob_func()
{
}

//-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//
// Section Attributes
//
//-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
#pragma section(".CRT$XCA",    long, read) // First C++ Initializer
#pragma section(".CRT$XCAA",   long, read) // Startup C++ Initializer
#pragma section(".CRT$XCZ",    long, read) // Last C++ Initializer

#pragma section(".CRT$XDA",    long, read) // First Dynamic TLS Initializer
#pragma section(".CRT$XDZ",    long, read) // Last Dynamic TLS Initializer

#pragma section(".CRT$XIA",    long, read) // First C Initializer
#pragma section(".CRT$XIAA",   long, read) // Startup C Initializer
#pragma section(".CRT$XIAB",   long, read) // PGO C Initializer
#pragma section(".CRT$XIAC",   long, read) // Post-PGO C Initializer
#pragma section(".CRT$XIC",    long, read) // CRT C Initializers
#pragma section(".CRT$XIYA",   long, read) // VCCorLib Threading Model Initializer
#pragma section(".CRT$XIYAA",  long, read) // XAML Designer Threading Model Override Initializer
#pragma section(".CRT$XIYB",   long, read) // VCCorLib Main Initializer
#pragma section(".CRT$XIZ",    long, read) // Last C Initializer

#pragma section(".CRT$XLA",    long, read) // First Loader TLS Callback
#pragma section(".CRT$XLC",    long, read) // CRT TLS Constructor
#pragma section(".CRT$XLD",    long, read) // CRT TLS Terminator
#pragma section(".CRT$XLZ",    long, read) // Last Loader TLS Callback

#pragma section(".CRT$XPA",    long, read) // First Pre-Terminator
#pragma section(".CRT$XPB",    long, read) // CRT ConcRT Pre-Terminator
#pragma section(".CRT$XPX",    long, read) // CRT Pre-Terminators
#pragma section(".CRT$XPXA",   long, read) // CRT stdio Pre-Terminator
#pragma section(".CRT$XPZ",    long, read) // Last Pre-Terminator

#pragma section(".CRT$XTA",    long, read) // First Terminator
#pragma section(".CRT$XTZ",    long, read) // Last Terminator

#pragma section(".CRTMA$XCA",  long, read) // First Managed C++ Initializer
#pragma section(".CRTMA$XCZ",  long, read) // Last Managed C++ Initializer

#pragma section(".CRTVT$XCA",  long, read) // First Managed VTable Initializer
#pragma section(".CRTVT$XCZ",  long, read) // Last Managed VTable Initializer

#pragma section(".rdata$T",    long, read)

#pragma section(".rtc$IAA",    long, read) // First RTC Initializer
#pragma section(".rtc$IZZ",    long, read) // Last RTC Initializer

#pragma section(".rtc$TAA",    long, read) // First RTC Terminator
#pragma section(".rtc$TZZ",    long, read) // Last RTC Terminator


extern "C" _CRTALLOC(".CRT$XIA") _PIFV __xi_a[] = { nullptr }; // C initializers (first)
extern "C" _CRTALLOC(".CRT$XIZ") _PIFV __xi_z[] = { nullptr }; // C initializers (last)
extern "C" _CRTALLOC(".CRT$XCA") _PVFV __xc_a[] = { nullptr }; // C++ initializers (first)
extern "C" _CRTALLOC(".CRT$XCZ") _PVFV __xc_z[] = { nullptr }; // C++ initializers (last)
extern "C" _CRTALLOC(".CRT$XPA") _PVFV __xp_a[] = { nullptr }; // C pre-terminators (first)
extern "C" _CRTALLOC(".CRT$XPZ") _PVFV __xp_z[] = { nullptr }; // C pre-terminators (last)
extern "C" _CRTALLOC(".CRT$XTA") _PVFV __xt_a[] = { nullptr }; // C terminators (first)
extern "C" _CRTALLOC(".CRT$XTZ") _PVFV __xt_z[] = { nullptr }; // C terminators (last)

#pragma comment(linker, "/merge:.CRT=.rdata")

int const StaticNotInitialized    = 0;
int const StaticInitialized  = -1;
int const EpochStart       = INT_MIN;

DWORD const XpTimeout = 100;

static _PVFV * ExitList;
static unsigned MaxExitListEntries;
static unsigned CurrentExitListIndex;
CRITICAL_SECTION _Tss_mutex;
CONDITION_VARIABLE _Tss_cv;

//NOTE: TLS Bullshit
extern "C"
{
    ULONG _tls_index = 0;
    
#pragma data_seg(".tls")
    _CRTALLOC(".tls")
        char _tls_start = 0;
    
#pragma data_seg(".tls$ZZZ")
    _CRTALLOC(".tls$ZZZ")
        char _tls_end = 0;
    
#pragma data_seg()
    
    
    //NOTE: Start of TLS callback generated by os loader code
    _CRTALLOC(".CRT$XLA") PIMAGE_TLS_CALLBACK __xl_a = 0;
    
    //NOTE: Terminator of TLS callback array
    _CRTALLOC(".CRT$XLZ") PIMAGE_TLS_CALLBACK __xl_z = 0;
    
    //NOTE: TLS array..
    _CRTALLOC(".rdata$T")
        extern const IMAGE_TLS_DIRECTORY64 _tls_used =
    {
        (ULONGLONG)  &_tls_start,
        (ULONGLONG)  &_tls_end,
        (ULONGLONG)  &_tls_index,
        (ULONGLONG) (&__xl_a + 1),
        (ULONG)0, 
        (LONG)0
    };
}

extern "C"
{
    int _Init_global_epoch = EpochStart;
    __declspec(thread) int _Init_thread_epoch = EpochStart;
}

static void _crt_init_atexit_tables(void)
{
    if(!ExitList)
    {
        MaxExitListEntries = 32;
        ExitList = (_PVFV*)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, 
                                     MaxExitListEntries*sizeof(_PVFV));
    }
}

static void _crt_clean_atexit_tables(void)
{
    if(ExitList)
    {
        MaxExitListEntries = 0;
        CurrentExitListIndex = 0;
        HeapFree(GetProcessHeap(), 0, ExitList);
    }
}

static void __cdecl _crt_thread_exit_func(void)
{
    DeleteCriticalSection(&_Tss_mutex);
}

static int __cdecl _crt_thread_init()
{
    InitializeCriticalSectionAndSpinCount(&_Tss_mutex, 4000);
    InitializeConditionVariable(&_Tss_cv);
    
    _crt_init_atexit_tables();
    
    atexit(_crt_thread_exit_func);
    return 0;
}

#pragma comment(linker, "/include:__scrt_initialize_tss_var")
extern "C" _CRTALLOC(".CRT$XIC") _PIFV __scrt_initialize_tss_var = _crt_thread_init;
static _CRTALLOC(".CRT$XDA") _PVFV __xd_a = nullptr;
static _CRTALLOC(".CRT$XDZ") _PVFV __xd_z = nullptr;


static int 
__crt_tls_exception_filter(unsigned long const Code)
{
    //NOTE: handle C++ exception
    if (Code == ('msc' | 0xE0000000))
    {
        return EXCEPTION_EXECUTE_HANDLER;
    }
    
    return EXCEPTION_CONTINUE_SEARCH;
}

// TRANSITION, toolset update
#pragma warning(push)
#pragma warning(disable:5030) // attribute '%s' is not recognized
/*
 * References to __tls_guard are generated by the compiler to detect if
 * dynamic initialization of TLS variables have run. This flag must be
 * set to 'true' before running the TLS initializers below.
 *
 * Note: [[msvc::no_tls_guard]] is not required here as it is known to
 * be statically initialized in this TU and all uses here will be unguarded
 * because of this. It is included for completion only but would be
 * necessary if a declaration is moved to a header and this variable
 * referenced from other files.
 */
[[msvc::no_tls_guard]] __declspec(thread) bool __tls_guard = false;
#pragma warning(pop)

void WINAPI __crt_tls_init_callback(PVOID, DWORD Reason, LPVOID)
{
    if(Reason != DLL_THREAD_ATTACH || __tls_guard == true)
    {
        return;
    }

    __tls_guard = true;
    
    {
        for(_PVFV *Func = &__xd_a + 1; Func != &__xd_z; ++Func)
        {
            if(*Func)
            {
                (*Func)();
            }
        }
    }
}

/*
* Define an initialized callback function pointer, so CRT startup code knows
* we have dynamically initialized __declspec(thread) variables that need to
* be initialized at process startup for the primary thread.
*/
extern const PIMAGE_TLS_CALLBACK __dyn_tls_init_callback = __crt_tls_init_callback;

/*
 * Enter a callback function pointer into the .CRT$XL* array, which is the
 * callback array pointed to by the IMAGE_TLS_DIRECTORY in the PE header, so
 * the OS knows we want to be notified on each thread startup/shutdown.
 */
static _CRTALLOC(".CRT$XLC") PIMAGE_TLS_CALLBACK __xl_c = __crt_tls_init_callback;

void __cdecl __dyn_tls_on_demand_init() noexcept
{
    __crt_tls_init_callback(nullptr, DLL_THREAD_ATTACH, nullptr);
}

//TODO: Thread Local Storage bullshit used by thread library....
// Still dows not working.....
extern "C" void 
__cdecl _Init_thread_lock(void)
{
    EnterCriticalSection(&_Tss_mutex);
}

extern "C" void 
__cdecl _Init_thread_unlock(void)
{
    LeaveCriticalSection(&_Tss_mutex);
}

extern "C" void 
__cdecl _Init_thread_wait(DWORD const TimeOut)
{
    SleepConditionVariableCS(&_Tss_cv, &_Tss_mutex, TimeOut);
    
    
    _Init_thread_lock();
    WaitForSingleObjectEx(0, TimeOut, FALSE);
    _Init_thread_unlock();
}

extern "C" void
__cdecl _Init_thread_notify(void)
{
    WakeAllConditionVariable(&_Tss_cv);
}

extern "C" void
__cdecl _Init_thread_header(int* const Static)
{
    
    _Init_thread_lock();
    
    if(*Static == StaticNotInitialized)
    
    {
        *Static = StaticInitialized;
    }
    else
    {
        //NOTE: Fix for WinXP....
        _Init_thread_wait(XpTimeout);
        while(*Static == StaticInitialized)
        {
            if(*Static == StaticNotInitialized)
            {
                *Static = StaticInitialized;
                _Init_thread_unlock();
            }
        }
        
        _Init_thread_epoch = _Init_global_epoch;
    }
    
    _Init_thread_unlock();
}

extern "C" void 
__cdecl _Init_thread_footer(int* const Static)
{
    _Init_thread_lock();
    {
        ++_Init_global_epoch;
        *Static = _Init_global_epoch;
    }
    _Init_thread_unlock();
    
    _Init_thread_notify();
}

extern "C" void 
__cdecl _Init_thread_abort(int* const Static)
{
    _Init_thread_lock();
    {
        *Static = StaticNotInitialized;
    }
    _Init_thread_unlock();
    
    _Init_thread_notify();
}

extern "C" int 
__cdecl _initterm_e (_PIFV * first, _PIFV * last)
{
    for (_PIFV* it = first; it != last; ++it)
    {
        if (*it == nullptr)
            continue;
        
        int const result = (**it)();
        if (result != 0)
            return result;
    }
    
    return 0;
}

extern "C" void 
__cdecl _initterm (_PVFV * first, _PVFV * last)
{
    for (_PVFV* it = first; it != last; ++it)
    {
        if (*it == nullptr)
            continue;
        
        (**it)();
    }
}

extern "C" int 
__cdecl atexit(_PVFV Func)
{
    if(CurrentExitListIndex < MaxExitListEntries)
    {
        ExitList[CurrentExitListIndex++] = Func;
        return 0;
    }
    
    return -1;
}

#pragma function(memset)
extern "C" void  *memset(void *Dest, int Value, size_t Size)
{
    unsigned char Val = *(unsigned char*)&Value;
    unsigned char *At = (unsigned char*)Dest;
    while(Size--)
    {
        *At++ = Val;
    }
    
    return Dest;
}

#pragma function(memcpy)
extern "C" void *memcpy(void *Dest, const void *Source, size_t Size)
{
    char *D = (char*)Dest;
    const char *S = (char*)Source;
    while(Size--)
    {
        *D++ = *S++;
    }
    
    return Dest;
}

#pragma function(memcmp)
extern "C" int memcmp(const void *A, const void *B, size_t Count)
{
    register const unsigned char *s1 = (const unsigned char*)A;
    register const unsigned char *s2 = (const unsigned char*)B;
    
    while (Count-- > 0)
    {
        if (*s1++ != *s2++)
        {
            return s1[-1] < s2[-1] ? -1 : 1;
        }
    }
    
    return 0;
}

void __stdcall __ehvec_ctor(
void*            ptr,           // Pointer to array to destruct
size_t           size,          // Size of each element (including padding)
size_t           count,         // Number of elements in the array
void (__stdcall *constructor)(void*),   // Constructor to call
void (__stdcall *destructor)(void*))
{
    size_t i{0};
    bool Success{false};
    
    {
        for (; i != count; ++i)
        {
            constructor(ptr);
            
            ptr = static_cast<char*>(ptr) + size;
        }
        
        Success = true;
    }
}

void __stdcall __ehvec_dtor(
void*           ptr,
size_t          size,
size_t          count,
void (__stdcall *destructor)(void*))
{
    int Success = 0;
    
    
    // Advance pointer past end of array
    ptr = (char*)(ptr) + size * count;
    
    {
        // Destruct elements
        while (count-- > 0)
        {
            ptr = (char*)(ptr) - size;
            destructor(ptr);
        }
        
        Success = 1;
    }
}

static void Win32CRTCall(_PVFV* a, _PVFV* b)
{
    while (a != b)
    {
        if (*a)
        {
            (**a)();
        }
        ++a;
    }
}

extern "C" void InitCRTEnvironment(void)
{
    _crt_init_atexit_tables();
    
    _initterm_e(__xi_a, __xi_z);
    _initterm(__xc_a, __xc_z);
}

extern "C" void ExitCRTEnvironment(void)
{
    
    if(CurrentExitListIndex)
    {
        _initterm(ExitList, ExitList + CurrentExitListIndex);
    }
    
    _crt_clean_atexit_tables();
    
    Win32CRTCall(__xp_a, __xp_z);
    Win32CRTCall(__xt_a, __xt_z);
}

extern "C"
{
    DWORD __CxxFrameHandler3(PEXCEPTION_RECORD rec, EXCEPTION_REGISTRATION_RECORD* ExceptionRegistrationFrame,
                             CONTEXT *Context, EXCEPTION_REGISTRATION_RECORD** Record)
    {
        return 0;
    }
}

#else

extern "C" void InitCRTEnvironment(void)
{
}

extern "C" void  ExitCRTEnvironment(void)
{
}

#endif
#endif // _WIN32
