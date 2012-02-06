;--------------------------------
; This script can be compiled into executable using nsis compiler (http://nsis.sourceforge.net)
; after publishing the addin from visual studio, run this script using nsis compiler, result would be one .exe file which has every thing packed in it.
; this script knows the publish directory relatively - chaning location of either this file or "publish" requires modifying this script.
; NOT SURE IF THIS WORKS FOR 32 BIT VISTA/7
;
; Author Hasanat Kazmi
;--------------------------------

!include LogicLib.nsh

Name "Open Images"
OutFile "installer.exe"

InstallDir $TEMP\nsisos

Var bits


Function osnotsupported
    DetailPrint "os is not supported"
	MessageBox MB_OK "You OS is not supported, please upgrade. Installer will abort now."
    Quit
FunctionEnd

Function GetServicePack
	Push $R0
	Push $R1
 
	ReadRegDWORD $R0 HKLM "System\CurrentControlSet\Control\Windows" "CSDVersion"
	IntOp $R1 $R0 % 256			;get minor version
	IntOp $R0 $R0 / 256			;get major version
 
	Exch $R1
	Exch
	Exch $R0
FunctionEnd

Function installspxp
	DetailPrint "Upgrading Service Pack required"
	MessageBox MB_OK "You have to update your Windows XP for installing Open Images. Please download and install Windows Xp Service Pack 2 or 3 for $bits from http://download.microsoft.com. Installer will quit now."
	Quit
FunctionEnd

Function installsp2007
	DetailPrint "Installing SP2 of Office 2007 required"
	MessageBox MB_OK "You have to install atleast Service Pack 2 for MS Office 2007. Please down and install it from http://download.microsoft.com. Installer will abort now"
	Quit
FunctionEnd

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;from http://nsis.sourceforge.net/StrContains;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; StrContains
; This function does a case sensitive searches for an occurrence of a substring in a string. 
; It returns the substring if it is found. 
; Otherwise it returns null(""). 
; Written by kenglish_hi
; Adapted from StrReplace written by dandaman32
 
 
Var STR_HAYSTACK
Var STR_NEEDLE
Var STR_CONTAINS_VAR_1
Var STR_CONTAINS_VAR_2
Var STR_CONTAINS_VAR_3
Var STR_CONTAINS_VAR_4
Var STR_RETURN_VAR
 
Function StrContains
  Exch $STR_NEEDLE
  Exch 1
  Exch $STR_HAYSTACK
  ; Uncomment to debug
  ;MessageBox MB_OK 'STR_NEEDLE = $STR_NEEDLE STR_HAYSTACK = $STR_HAYSTACK '
    StrCpy $STR_RETURN_VAR ""
    StrCpy $STR_CONTAINS_VAR_1 -1
    StrLen $STR_CONTAINS_VAR_2 $STR_NEEDLE
    StrLen $STR_CONTAINS_VAR_4 $STR_HAYSTACK
    loop:
      IntOp $STR_CONTAINS_VAR_1 $STR_CONTAINS_VAR_1 + 1
      StrCpy $STR_CONTAINS_VAR_3 $STR_HAYSTACK $STR_CONTAINS_VAR_2 $STR_CONTAINS_VAR_1
      StrCmp $STR_CONTAINS_VAR_3 $STR_NEEDLE found
      StrCmp $STR_CONTAINS_VAR_1 $STR_CONTAINS_VAR_4 done
      Goto loop
    found:
      StrCpy $STR_RETURN_VAR $STR_NEEDLE
      Goto done
    done:
   Pop $STR_NEEDLE ;Prevent "invalid opcode" errors and keep the
   Exch $STR_RETURN_VAR  
FunctionEnd
 
!macro _StrContainsConstructor OUT NEEDLE HAYSTACK
  Push "${HAYSTACK}"
  Push "${NEEDLE}"
  Call StrContains
  Pop "${OUT}"
!macroend
 
!define StrContains '!insertmacro "_StrContainsConstructor"'

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;


Section 

; detecting OS
File /oname=$TEMP\nsisos.dll nsisos.dll
CallInstDLL $TEMP\nsisos.dll osversion
StrCpy $R0 $0
StrCpy $R1 $1
CallInstDLL $TEMP\nsisos.dll osplatform
StrCpy $R2 $0
;DetailPrint "Major Version: $R0 $\nMinor Version: $R1 $\nPlatform: $R2"

${If} $R0 == "5"  ;Windows 2000, Windows XP or Windows Server 2003
  ${If} $R1 == "0"
	;Windows 2000
	DetailPrint "Windows 2000 is not supported, Aborting"
	Call osnotsupported
  ${ElseIf} $R1 == "1";win xp 32 bit
	DetailPrint "You have Windows XP 32 bit"
	StrCpy $bits "32"
	Call GetServicePack
	Pop $R0
	Pop $R1
	DetailPrint "Your Windows XP Service Pack is $R0"
	${If} $R0 == "0" ;xp32 with sp0
	  ;update ur sp
	  Call installspxp
	${ElseIf} $R0 == "1";xp32 with sp1
	  ;update sp
	  Call installspxp
	${EndIf}  

  ${ElseIf} $R1 == "2";win xp 64 bit
	DetailPrint "You have Windows XP 64 bit"
	StrCpy $bits "64"
	Call GetServicePack
	Pop $R0
	Pop $R1
	DetailPrint "Your Windows XP Service Pack is $R0"
	${If} $R0 == "0" ; xp64 with sp0
	  ;update ur sp
	  Call installspxp
	${ElseIf} $R0 == "1" ; xp64 with sp1
	  ;update sp
	  Call installspxp
	${EndIf}  

  ${EndIf}
${ElseIf} $R0 == "6" ;Windows Vista, Windows Server 2008   6 (including R2 update) or Windows 7 
  ;currently we don't exactly know if Open Images need SP1 installed or not, but perhaps sp for office would install that automatically.
  ;DetailPrint "6 hai"
  Call GetServicePack
  Pop $R0
  Pop $R1
  ;DetailPrint "major version $R0, minor version $R1"
  DetailPrint "You have Windows Vista/7 SP$R0 installed"
${EndIf}

DetailPrint "OS version Compatible"

;;;;;;; script has reached this point, it means OS has passed for installation.

;This registry location is only tested on windows 7, no idea about other versions specially XP
ReadRegStr $R0 HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\powerpnt.exe" "Path"
;IfErrors .... how to print now? supposing it won't raise error
;DetailPrint "path is $R0"

${StrContains} $0 "Office14" $R0
${If} $0 != "" ;i.e. its office 14
	DetailPrint "Office 2010 found"
	;if Office 2010 is installed on vista/7
	ReadRegStr $R0 HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Office\14.0\Common\ProductVersion" "LastProduct"
	DetailPrint "Office 2010 version is $R0"  ; corrently all sp of office 2010 are supported
${Else}
	${StrContains} $0 "Office12" $R0
	${If} $0 != "" ;i.e. its office 12
		DetailPrint "Office 2007 found"
		; if office 2007 is installed on vista/7
		ReadRegStr $R0 HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Office\12.0\Common\ProductVersion" "LastProduct"
		DetailPrint "Office 2007 version is $R0"
		${StrContains} $0 "12.0.4" $R0
		${If} $0 != "" ;i.e. its sp0
			;get user install sp2
			DetailPrint "Office 2007 version is $R0, you must upgrade Office to Service Pack 2"
			Call installsp2007
		${Else}
			${StrContains} $0 "12.0.62" $R0
			${If} $0 != "" ;i.e. its sp1
				;get user install sp2
				DetailPrint "Office 2007 version is $R0, you must upgrade Office to Service Pack 2"
				Call installsp2007
			${EndIf}	
		${EndIf}
		
	${Else}
		DetailPrint "Office 2007 or Office 2010 not found."
		MessageBox MB_OK "You don't have any compatible version of MS Office Installer. We currently support Office 2007 and Office 2010. Installer will abort now."
		Quit
	${EndIf}
${EndIf}

DetailPrint "MS Office is compatabile."
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;
DetailPrint "All Pre-reqs met, unpacking installation files....."
;Delete $TEMP\nsisos.dll
CreateDirectory $TEMP\openimages
SetOutPath $TEMP\openimages
File /r ..\publish\*.* 
MessageBox MB_OK "Your computer and all installed software is compatable for Open Images, click OK to continue"
ExecWait $OUTDIR\setup.exe
Quit

SectionEnd

