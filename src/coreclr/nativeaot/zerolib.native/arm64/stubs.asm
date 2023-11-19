#include <ksarm64.h>

TEXTAREA

    LEAF_ENTRY RhpAssignRefArm64, _TEXT
    stlr    x15, [x14]
    add     x14, x14, 8
    ret
    LEAF_END RhpAssignRefArm64

    LEAF_ENTRY RhpCheckedAssignRefArm64, _TEXT
    str     x15, [x14], 8
    ret
    LEAF_END RhpCheckedAssignRefArm64

END
