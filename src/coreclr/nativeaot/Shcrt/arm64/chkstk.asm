#include <ksarm64.h>

TEXTAREA

    LEAF_ENTRY __chkstk, _TEXT

    ldr         xip1,[xpr,#0x10]
    subs        xip0,sp,x15,lsl #4
    csello      xip0,xzr,xip0
    cmp         xip0,xip1
    blo         LOC0
    ret
LOC0
    and         xip0,xip0,#-0x1000
LOC1
    sub         xip1,xip1,#1,lsl #0xC
    ldr         xzr,[xip1]
    cmp         xip1,xip0
    bne         LOC1
    ret

    LEAF_END __chkstk

END
