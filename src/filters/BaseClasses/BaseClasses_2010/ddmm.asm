﻿; Listing generated by Microsoft (R) Optimizing Compiler Version 16.00.30319.01 

	TITLE	C:\Downloads\MediaPoint\src\filters\BaseClasses\ddmm.cpp
	.686P
	.XMM
	include listing.inc
	.model	flat

INCLUDELIB LIBCMTD
INCLUDELIB OLDNAMES

PUBLIC	_IID_IAMFilterGraphCallback
;	COMDAT _IID_IAMFilterGraphCallback
CONST	SEGMENT
_IID_IAMFilterGraphCallback DD 056a868fdH
	DW	0ad4H
	DW	011ceH
	DB	0b0H
	DB	0a3H
	DB	00H
	DB	020H
	DB	0afH
	DB	0bH
	DB	0a7H
	DB	070H
CONST	ENDS
PUBLIC	?FindDeviceCallback@@YGHPAU_GUID@@PAD1PAX@Z	; FindDeviceCallback
EXTRN	__imp__lstrcmpiA@8:PROC
EXTRN	__RTC_CheckEsp:PROC
EXTRN	__RTC_Shutdown:PROC
EXTRN	__RTC_InitBase:PROC
;	COMDAT rtc$TMZ
; File c:\downloads\mediapoint\src\filters\baseclasses\ddmm.cpp
rtc$TMZ	SEGMENT
__RTC_Shutdown.rtc$TMZ DD FLAT:__RTC_Shutdown
rtc$TMZ	ENDS
;	COMDAT rtc$IMZ
rtc$IMZ	SEGMENT
__RTC_InitBase.rtc$IMZ DD FLAT:__RTC_InitBase
; Function compile flags: /Odtp /RTCsu
rtc$IMZ	ENDS
;	COMDAT ?FindDeviceCallback@@YGHPAU_GUID@@PAD1PAX@Z
_TEXT	SEGMENT
_p$ = -4						; size = 4
_lpGUID$ = 8						; size = 4
_szName$ = 12						; size = 4
_szDevice$ = 16						; size = 4
_lParam$ = 20						; size = 4
?FindDeviceCallback@@YGHPAU_GUID@@PAD1PAX@Z PROC	; FindDeviceCallback, COMDAT
; Line 26
	push	ebp
	mov	ebp, esp
	push	ecx
	push	esi
	mov	DWORD PTR [ebp-4], -858993460		; ccccccccH
; Line 27
	mov	eax, DWORD PTR _lParam$[ebp]
	mov	DWORD PTR _p$[ebp], eax
; Line 29
	mov	esi, esp
	mov	ecx, DWORD PTR _szDevice$[ebp]
	push	ecx
	mov	edx, DWORD PTR _p$[ebp]
	mov	eax, DWORD PTR [edx]
	push	eax
	call	DWORD PTR __imp__lstrcmpiA@8
	cmp	esi, esp
	call	__RTC_CheckEsp
	test	eax, eax
	jne	SHORT $LN3@FindDevice
; Line 30
	cmp	DWORD PTR _lpGUID$[ebp], 0
	je	SHORT $LN2@FindDevice
; Line 31
	mov	ecx, DWORD PTR _lpGUID$[ebp]
	mov	edx, DWORD PTR _p$[ebp]
	add	edx, 8
	mov	eax, DWORD PTR [ecx]
	mov	DWORD PTR [edx], eax
	mov	eax, DWORD PTR [ecx+4]
	mov	DWORD PTR [edx+4], eax
	mov	eax, DWORD PTR [ecx+8]
	mov	DWORD PTR [edx+8], eax
	mov	ecx, DWORD PTR [ecx+12]
	mov	DWORD PTR [edx+12], ecx
; Line 32
	mov	edx, DWORD PTR _p$[ebp]
	add	edx, 8
	mov	eax, DWORD PTR _p$[ebp]
	mov	DWORD PTR [eax+4], edx
; Line 33
	jmp	SHORT $LN1@FindDevice
$LN2@FindDevice:
; Line 34
	mov	ecx, DWORD PTR _p$[ebp]
	mov	DWORD PTR [ecx+4], 0
$LN1@FindDevice:
; Line 36
	mov	edx, DWORD PTR _p$[ebp]
	mov	DWORD PTR [edx+24], 1
; Line 37
	xor	eax, eax
	jmp	SHORT $LN4@FindDevice
$LN3@FindDevice:
; Line 39
	mov	eax, 1
$LN4@FindDevice:
; Line 40
	pop	esi
	add	esp, 4
	cmp	ebp, esp
	call	__RTC_CheckEsp
	mov	esp, ebp
	pop	ebp
	ret	16					; 00000010H
?FindDeviceCallback@@YGHPAU_GUID@@PAD1PAX@Z ENDP	; FindDeviceCallback
_TEXT	ENDS
PUBLIC	?FindDeviceCallbackEx@@YGHPAU_GUID@@PAD1PAXPAUHMONITOR__@@@Z ; FindDeviceCallbackEx
; Function compile flags: /Odtp /RTCsu
;	COMDAT ?FindDeviceCallbackEx@@YGHPAU_GUID@@PAD1PAXPAUHMONITOR__@@@Z
_TEXT	SEGMENT
_p$ = -4						; size = 4
_lpGUID$ = 8						; size = 4
_szName$ = 12						; size = 4
_szDevice$ = 16						; size = 4
_lParam$ = 20						; size = 4
_hMonitor$ = 24						; size = 4
?FindDeviceCallbackEx@@YGHPAU_GUID@@PAD1PAXPAUHMONITOR__@@@Z PROC ; FindDeviceCallbackEx, COMDAT
; Line 44
	push	ebp
	mov	ebp, esp
	push	ecx
	push	esi
	mov	DWORD PTR [ebp-4], -858993460		; ccccccccH
; Line 45
	mov	eax, DWORD PTR _lParam$[ebp]
	mov	DWORD PTR _p$[ebp], eax
; Line 47
	mov	esi, esp
	mov	ecx, DWORD PTR _szDevice$[ebp]
	push	ecx
	mov	edx, DWORD PTR _p$[ebp]
	mov	eax, DWORD PTR [edx]
	push	eax
	call	DWORD PTR __imp__lstrcmpiA@8
	cmp	esi, esp
	call	__RTC_CheckEsp
	test	eax, eax
	jne	SHORT $LN3@FindDevice@2
; Line 48
	cmp	DWORD PTR _lpGUID$[ebp], 0
	je	SHORT $LN2@FindDevice@2
; Line 49
	mov	ecx, DWORD PTR _lpGUID$[ebp]
	mov	edx, DWORD PTR _p$[ebp]
	add	edx, 8
	mov	eax, DWORD PTR [ecx]
	mov	DWORD PTR [edx], eax
	mov	eax, DWORD PTR [ecx+4]
	mov	DWORD PTR [edx+4], eax
	mov	eax, DWORD PTR [ecx+8]
	mov	DWORD PTR [edx+8], eax
	mov	ecx, DWORD PTR [ecx+12]
	mov	DWORD PTR [edx+12], ecx
; Line 50
	mov	edx, DWORD PTR _p$[ebp]
	add	edx, 8
	mov	eax, DWORD PTR _p$[ebp]
	mov	DWORD PTR [eax+4], edx
; Line 51
	jmp	SHORT $LN1@FindDevice@2
$LN2@FindDevice@2:
; Line 52
	mov	ecx, DWORD PTR _p$[ebp]
	mov	DWORD PTR [ecx+4], 0
$LN1@FindDevice@2:
; Line 54
	mov	edx, DWORD PTR _p$[ebp]
	mov	DWORD PTR [edx+24], 1
; Line 55
	xor	eax, eax
	jmp	SHORT $LN4@FindDevice@2
$LN3@FindDevice@2:
; Line 57
	mov	eax, 1
$LN4@FindDevice@2:
; Line 58
	pop	esi
	add	esp, 4
	cmp	ebp, esp
	call	__RTC_CheckEsp
	mov	esp, ebp
	pop	ebp
	ret	20					; 00000014H
?FindDeviceCallbackEx@@YGHPAU_GUID@@PAD1PAXPAUHMONITOR__@@@Z ENDP ; FindDeviceCallbackEx
_TEXT	ENDS
PUBLIC	__$ArrayPad$
PUBLIC	_DirectDrawCreateFromDevice@12
EXTRN	__imp__SetErrorMode@4:PROC
EXTRN	___security_cookie:DWORD
EXTRN	@__security_check_cookie@4:PROC
EXTRN	@_RTC_CheckStackVars@8:PROC
; Function compile flags: /Odtp /RTCsu
;	COMDAT _DirectDrawCreateFromDevice@12
_TEXT	SEGMENT
_ErrorMode$81616 = -56					; size = 4
_find$ = -48						; size = 28
_pdd$ = -12						; size = 4
__$ArrayPad$ = -4					; size = 4
_szDevice$ = 8						; size = 4
_DirectDrawCreateP$ = 12				; size = 4
_DirectDrawEnumerateP$ = 16				; size = 4
_DirectDrawCreateFromDevice@12 PROC			; COMDAT
; Line 67
	push	ebp
	mov	ebp, esp
	sub	esp, 56					; 00000038H
	push	esi
	push	edi
	lea	edi, DWORD PTR [ebp-56]
	mov	ecx, 14					; 0000000eH
	mov	eax, -858993460				; ccccccccH
	rep stosd
	mov	eax, DWORD PTR ___security_cookie
	xor	eax, ebp
	mov	DWORD PTR __$ArrayPad$[ebp], eax
; Line 68
	mov	DWORD PTR _pdd$[ebp], 0
; Line 71
	cmp	DWORD PTR _szDevice$[ebp], 0
	jne	SHORT $LN2@DirectDraw
; Line 72
	mov	esi, esp
	push	0
	lea	eax, DWORD PTR _pdd$[ebp]
	push	eax
	push	0
	call	DWORD PTR _DirectDrawCreateP$[ebp]
	cmp	esi, esp
	call	__RTC_CheckEsp
; Line 73
	mov	eax, DWORD PTR _pdd$[ebp]
	jmp	SHORT $LN3@DirectDraw
$LN2@DirectDraw:
; Line 76
	mov	ecx, DWORD PTR _szDevice$[ebp]
	mov	DWORD PTR _find$[ebp], ecx
; Line 77
	mov	DWORD PTR _find$[ebp+24], 0
; Line 78
	mov	esi, esp
	lea	edx, DWORD PTR _find$[ebp]
	push	edx
	push	OFFSET ?FindDeviceCallback@@YGHPAU_GUID@@PAD1PAX@Z ; FindDeviceCallback
	call	DWORD PTR _DirectDrawEnumerateP$[ebp]
	cmp	esi, esp
	call	__RTC_CheckEsp
; Line 80
	cmp	DWORD PTR _find$[ebp+24], 0
	je	SHORT $LN1@DirectDraw
; Line 87
	mov	esi, esp
	push	1
	call	DWORD PTR __imp__SetErrorMode@4
	cmp	esi, esp
	call	__RTC_CheckEsp
	mov	DWORD PTR _ErrorMode$81616[ebp], eax
; Line 88
	mov	esi, esp
	push	0
	lea	eax, DWORD PTR _pdd$[ebp]
	push	eax
	mov	ecx, DWORD PTR _find$[ebp+4]
	push	ecx
	call	DWORD PTR _DirectDrawCreateP$[ebp]
	cmp	esi, esp
	call	__RTC_CheckEsp
; Line 89
	mov	esi, esp
	mov	edx, DWORD PTR _ErrorMode$81616[ebp]
	push	edx
	call	DWORD PTR __imp__SetErrorMode@4
	cmp	esi, esp
	call	__RTC_CheckEsp
$LN1@DirectDraw:
; Line 92
	mov	eax, DWORD PTR _pdd$[ebp]
$LN3@DirectDraw:
; Line 93
	push	edx
	mov	ecx, ebp
	push	eax
	lea	edx, DWORD PTR $LN8@DirectDraw
	call	@_RTC_CheckStackVars@8
	pop	eax
	pop	edx
	pop	edi
	pop	esi
	mov	ecx, DWORD PTR __$ArrayPad$[ebp]
	xor	ecx, ebp
	call	@__security_check_cookie@4
	add	esp, 56					; 00000038H
	cmp	ebp, esp
	call	__RTC_CheckEsp
	mov	esp, ebp
	pop	ebp
	ret	12					; 0000000cH
$LN8@DirectDraw:
	DD	2
	DD	$LN7@DirectDraw
$LN7@DirectDraw:
	DD	-12					; fffffff4H
	DD	4
	DD	$LN5@DirectDraw
	DD	-48					; ffffffd0H
	DD	28					; 0000001cH
	DD	$LN6@DirectDraw
$LN6@DirectDraw:
	DB	102					; 00000066H
	DB	105					; 00000069H
	DB	110					; 0000006eH
	DB	100					; 00000064H
	DB	0
$LN5@DirectDraw:
	DB	112					; 00000070H
	DB	100					; 00000064H
	DB	100					; 00000064H
	DB	0
_DirectDrawCreateFromDevice@12 ENDP
_TEXT	ENDS
PUBLIC	__$ArrayPad$
PUBLIC	_DirectDrawCreateFromDeviceEx@12
; Function compile flags: /Odtp /RTCsu
;	COMDAT _DirectDrawCreateFromDeviceEx@12
_TEXT	SEGMENT
_ErrorMode$81627 = -56					; size = 4
_find$ = -48						; size = 28
_pdd$ = -12						; size = 4
__$ArrayPad$ = -4					; size = 4
_szDevice$ = 8						; size = 4
_DirectDrawCreateP$ = 12				; size = 4
_DirectDrawEnumerateExP$ = 16				; size = 4
_DirectDrawCreateFromDeviceEx@12 PROC			; COMDAT
; Line 102
	push	ebp
	mov	ebp, esp
	sub	esp, 56					; 00000038H
	push	esi
	push	edi
	lea	edi, DWORD PTR [ebp-56]
	mov	ecx, 14					; 0000000eH
	mov	eax, -858993460				; ccccccccH
	rep stosd
	mov	eax, DWORD PTR ___security_cookie
	xor	eax, ebp
	mov	DWORD PTR __$ArrayPad$[ebp], eax
; Line 103
	mov	DWORD PTR _pdd$[ebp], 0
; Line 106
	cmp	DWORD PTR _szDevice$[ebp], 0
	jne	SHORT $LN2@DirectDraw@2
; Line 107
	mov	esi, esp
	push	0
	lea	eax, DWORD PTR _pdd$[ebp]
	push	eax
	push	0
	call	DWORD PTR _DirectDrawCreateP$[ebp]
	cmp	esi, esp
	call	__RTC_CheckEsp
; Line 108
	mov	eax, DWORD PTR _pdd$[ebp]
	jmp	SHORT $LN3@DirectDraw@2
$LN2@DirectDraw@2:
; Line 111
	mov	ecx, DWORD PTR _szDevice$[ebp]
	mov	DWORD PTR _find$[ebp], ecx
; Line 112
	mov	DWORD PTR _find$[ebp+24], 0
; Line 114
	mov	esi, esp
	push	1
	lea	edx, DWORD PTR _find$[ebp]
	push	edx
	push	OFFSET ?FindDeviceCallbackEx@@YGHPAU_GUID@@PAD1PAXPAUHMONITOR__@@@Z ; FindDeviceCallbackEx
	call	DWORD PTR _DirectDrawEnumerateExP$[ebp]
	cmp	esi, esp
	call	__RTC_CheckEsp
; Line 116
	cmp	DWORD PTR _find$[ebp+24], 0
	je	SHORT $LN1@DirectDraw@2
; Line 123
	mov	esi, esp
	push	1
	call	DWORD PTR __imp__SetErrorMode@4
	cmp	esi, esp
	call	__RTC_CheckEsp
	mov	DWORD PTR _ErrorMode$81627[ebp], eax
; Line 124
	mov	esi, esp
	push	0
	lea	eax, DWORD PTR _pdd$[ebp]
	push	eax
	mov	ecx, DWORD PTR _find$[ebp+4]
	push	ecx
	call	DWORD PTR _DirectDrawCreateP$[ebp]
	cmp	esi, esp
	call	__RTC_CheckEsp
; Line 125
	mov	esi, esp
	mov	edx, DWORD PTR _ErrorMode$81627[ebp]
	push	edx
	call	DWORD PTR __imp__SetErrorMode@4
	cmp	esi, esp
	call	__RTC_CheckEsp
$LN1@DirectDraw@2:
; Line 128
	mov	eax, DWORD PTR _pdd$[ebp]
$LN3@DirectDraw@2:
; Line 129
	push	edx
	mov	ecx, ebp
	push	eax
	lea	edx, DWORD PTR $LN8@DirectDraw@2
	call	@_RTC_CheckStackVars@8
	pop	eax
	pop	edx
	pop	edi
	pop	esi
	mov	ecx, DWORD PTR __$ArrayPad$[ebp]
	xor	ecx, ebp
	call	@__security_check_cookie@4
	add	esp, 56					; 00000038H
	cmp	ebp, esp
	call	__RTC_CheckEsp
	mov	esp, ebp
	pop	ebp
	ret	12					; 0000000cH
	npad	2
$LN8@DirectDraw@2:
	DD	2
	DD	$LN7@DirectDraw@2
$LN7@DirectDraw@2:
	DD	-12					; fffffff4H
	DD	4
	DD	$LN5@DirectDraw@2
	DD	-48					; ffffffd0H
	DD	28					; 0000001cH
	DD	$LN6@DirectDraw@2
$LN6@DirectDraw@2:
	DB	102					; 00000066H
	DB	105					; 00000069H
	DB	110					; 0000006eH
	DB	100					; 00000064H
	DB	0
$LN5@DirectDraw@2:
	DB	112					; 00000070H
	DB	100					; 00000064H
	DB	100					; 00000064H
	DB	0
_DirectDrawCreateFromDeviceEx@12 ENDP
_TEXT	ENDS
END
