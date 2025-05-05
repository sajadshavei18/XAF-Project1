;#DropDB XAF_Project1EasyTest

#Application XAF_Project1.Win
#Application XAF_Project1.Web
#IfDef XAF_Project1.Win

*Action About...
 
*Action OK

#EndIf

#IfDef XAF_Project1.Web
!Action Navigation(My Details)
#EndIf
