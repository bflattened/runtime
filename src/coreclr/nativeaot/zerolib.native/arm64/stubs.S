
.macro LEAF_ENTRY Name, Section
        .global \Name
        .hidden \Name
        .type \Name, %function
\Name:
        .cfi_startproc
.endm

.macro LEAF_END Name, Section
        .size \Name, .-\Name
        .cfi_endproc
.endm

LEAF_ENTRY RhpAssignRefArm64, _TEXT
        stlr    x15, [x14]
        add     x14, x14, 8
        ret
LEAF_END RhpAssignRefArm64, _TEXT

LEAF_ENTRY RhpCheckedAssignRefArm64, _TEXT
        str     x15, [x14], 8
        ret
LEAF_END RhpCheckedAssignRefArm64, _TEXT
