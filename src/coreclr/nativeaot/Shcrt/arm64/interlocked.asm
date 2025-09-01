#include <ksarm64.h>

TEXTAREA

    LEAF_ENTRY _InterlockedCompareExchange128, _TEXT
    ldp     x6, x7, [x3]
fallback_loop
    ldaxp   x4, x5, [x0]
    cmp     x4, x6
    ccmpeq  x5, x7, #0
    bne     fallback_exit
    stlxp   wip0, x2, x1, [x0]
    cbnz    wip0, fallback_loop
fallback_exit
    stp     x4, x5, [x3]
    cseteq  x0
    ret
    LEAF_END _InterlockedCompareExchange128
END
