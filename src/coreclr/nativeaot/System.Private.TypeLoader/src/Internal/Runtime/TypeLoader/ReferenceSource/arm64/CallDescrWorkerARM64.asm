; Licensed to the .NET Foundation under one or more agreements.
; The .NET Foundation licenses this file to you under the MIT license.

#include "ksarm64.h"

#include "asmconstants.h"



    IMPORT CallDescrWorkerUnwindFrameChainHandler
;;-----------------------------------------------------------------------------
;; This helper routine enregisters the appropriate arguments and makes the
;; actual call.
;;-----------------------------------------------------------------------------
;;void CallDescrWorkerInternal(CallDescrData * pCallDescrData);
        NESTED_ENTRY CallDescrWorkerInternal,,CallDescrWorkerUnwindFrameChainHandler
        PROLOG_SAVE_REG_PAIR   fp, lr, #-32!
        PROLOG_SAVE_REG        x19, #16   ;the stack slot at sp+24 is empty for 16 byte alligment

        mov     x19, x0 ; save pCallDescrData in x19

        ldr     w1, [x19,#CallDescrData__numStackSlots]
        cbz     w1, Ldonestack

        ;; Add frame padding to ensure frame size is a multiple of 16 (a requirement of the OS ABI).
        ;; We push two registers (above) and numStackSlots arguments (below). If this comes to an odd number
        ;; of slots we must pad with another. This simplifies to "if the low bit of numStackSlots is set,
        ;; extend the stack another eight bytes".
        ldr     x0, [x19,#CallDescrData__pSrc]
        add     x0, x0, x1 lsl #3               ; pSrcEnd=pSrc+8*numStackSlots 
        ands    x2, x1, #1
        beq     Lstackloop
    
        ;; This loop copies numStackSlots words
        ;; from [pSrcEnd-8,pSrcEnd-16,...] to [sp-8,sp-16,...]

        ;; pad and store one stack slot as number of slots are odd
        ldr     x4, [x0,#-8]!
        str     x4, [sp,#-16]!
        subs    x1, x1, #1
        beq     Ldonestack   
Lstackloop
        ldp     x2, x4, [x0,#-16]!
        stp     x2, x4, [sp,#-16]!
        subs    x1, x1, #2
        bne     Lstackloop
Ldonestack

        ;; If FP arguments are supplied in registers (x8 != NULL) then initialize all of them from the pointer
        ;; given in x8. 
        ldr     x8, [x19,#CallDescrData__pFloatArgumentRegisters]
        cbz     x8, LNoFloatingPoint
        ldp     d0, d1, [x8]
        ldp     d2, d3, [x8, #16]
        ldp     d4, d5, [x8, #32]
        ldp     d6, d7, [x8, #48]
LNoFloatingPoint

        ;; Copy [pArgumentRegisters, ..., pArgumentRegisters + 56]
        ;; into x0, ..., x7

        ldr     x8, [x19,#CallDescrData__pArgumentRegisters]
        ldp     x0, x1, [x8]
        ldp     x2, x3, [x8, #16]
        ldp     x4, x5, [x8, #32]
        ldp     x6, x7, [x8, #48]

        ;; ARM64TODO: => see if anything special needs to be done for remoting
        ;; call pTarget
        ldr     x8, [x19,#CallDescrData__pTarget]
        blr     x8

        ldr     w3, [x19,#CallDescrData__fpReturnSize]

        ;; Int return case
        cbz     w3, LIntReturn

        ;; Float return case
        cmp     w3, #4
        beq     LFloatReturn

        ;; Double return case
        cmp     w3, #8
        bne     LNoDoubleReturn

LFloatReturn
        str     d0, [x19, #(CallDescrData__returnValue + 0)]
        b       LReturnDone

LNoDoubleReturn

        ;;FloatHFAReturn  return case
        cmp     w3, #16
        bne     LNoFloatHFAReturn

        stp     s0, s1, [x19, #(CallDescrData__returnValue + 0)]
        stp     s2, s3, [x19, #(CallDescrData__returnValue + 0x08)]
        b       LReturnDone
LNoFloatHFAReturn

        ;;DoubleHFAReturn  return case
        cmp     w3, #32
        bne     LNoDoubleHFAReturn

        stp     d0, d1, [x19, #(CallDescrData__returnValue + 0)]
        stp     d2, d3, [x19, #(CallDescrData__returnValue + 0x10)]
        b       LReturnDone

LNoDoubleHFAReturn

        EMIT_BREAKPOINT ; Unreachable

LIntReturn
        ;; Save return value into retbuf for int
        str     x0, [x19, #(CallDescrData__returnValue + 0)]

LReturnDone

#ifdef _DEBUG
        ;; trash the floating point registers to ensure that the HFA return values 
        ;; won't survive by accident
        ldp     d0, d1, [sp]
        ldp     d2, d3, [sp, #16]
#endif

        EPILOG_STACK_RESTORE
        EPILOG_RESTORE_REG      x19, #16    ;the stack slot at sp+24 is empty for 16 byte alligment
        EPILOG_RESTORE_REG_PAIR fp, lr, #32!
        EPILOG_RETURN
        NESTED_END

    END
