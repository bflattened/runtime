.586
.xmm
.model  flat

ALTERNATE_ENTRY macro Name

Name label proc
PUBLIC Name
endm

.code

PUBLIC RhpByRefAssignRef
RhpByRefAssignRef PROC
  movs    dword ptr es:[edi],dword ptr [esi]  
  ret
RhpByRefAssignRef ENDP

PUBLIC RhpCheckedAssignRefEAX
ALTERNATE_ENTRY RhpAssignRefEAX
RhpCheckedAssignRefEAX PROC
  mov     DWORD PTR [edx], eax
  ret
RhpCheckedAssignRefEAX ENDP

PUBLIC RhpCheckedAssignRefEBX
ALTERNATE_ENTRY RhpAssignRefEBX
RhpCheckedAssignRefEBX PROC
  mov     DWORD PTR [edx], ebx
  ret
RhpCheckedAssignRefEBX ENDP

PUBLIC RhpCheckedAssignRefECX
ALTERNATE_ENTRY RhpAssignRefECX
RhpCheckedAssignRefECX PROC
  mov     DWORD PTR [edx], ecx
  ret
RhpCheckedAssignRefECX ENDP

PUBLIC RhpCheckedAssignRefESI
ALTERNATE_ENTRY RhpAssignRefESI
RhpCheckedAssignRefESI PROC
  mov     DWORD PTR [edx], esi
  ret
RhpCheckedAssignRefESI ENDP

PUBLIC RhpCheckedAssignRefEDI
ALTERNATE_ENTRY RhpAssignRefEDI
RhpCheckedAssignRefEDI PROC
  mov     DWORD PTR [edx], edi
  ret
RhpCheckedAssignRefEDI ENDP

PUBLIC RhpCheckedAssignRefEBP
ALTERNATE_ENTRY RhpAssignRefEBP
RhpCheckedAssignRefEBP PROC
  mov     DWORD PTR [edx], ebp
  ret
RhpCheckedAssignRefEBP ENDP

end
