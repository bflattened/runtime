.386
.model flat
.code

__chkstk proc

push        ecx
lea         ecx,[esp+4]
sub         ecx,eax
sbb         eax,eax
not         eax
and         ecx,eax
mov         eax,esp
and         eax,0FFFFF000h
cs10:
cmp         ecx,eax
jb          cs20
mov         eax,ecx
pop         ecx
xchg        eax,esp
mov         eax,dword ptr [eax]
mov         dword ptr [esp],eax
ret
cs20:
sub         eax,1000h
test        dword ptr [eax],eax
jmp         cs10

__chkstk endp
end
