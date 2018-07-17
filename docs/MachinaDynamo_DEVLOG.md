```text
//  ███╗   ███╗ █████╗  ██████╗██╗  ██╗██╗███╗   ██╗ █████╗
//  ████╗ ████║██╔══██╗██╔════╝██║  ██║██║████╗  ██║██╔══██╗
//  ██╔████╔██║███████║██║     ███████║██║██╔██╗ ██║███████║
//  ██║╚██╔╝██║██╔══██║██║     ██╔══██║██║██║╚██╗██║██╔══██║
//  ██║ ╚═╝ ██║██║  ██║╚██████╗██║  ██║██║██║ ╚████║██║  ██║
//  ╚═╝     ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝
//                                                          
//  ██████╗ ██╗   ██╗███╗   ██╗ █████╗ ███╗   ███╗ ██████╗
//  ██╔══██╗╚██╗ ██╔╝████╗  ██║██╔══██╗████╗ ████║██╔═══██╗
//  ██║  ██║ ╚████╔╝ ██╔██╗ ██║███████║██╔████╔██║██║   ██║
//  ██║  ██║  ╚██╔╝  ██║╚██╗██║██╔══██║██║╚██╔╝██║██║   ██║
//  ██████╔╝   ██║   ██║ ╚████║██║  ██║██║ ╚═╝ ██║╚██████╔╝
//  ╚═════╝    ╚═╝   ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝     ╚═╝ ╚═════╝
//                                                         
//  ██████╗ ███████╗██╗   ██╗██╗      ██████╗  ██████╗      
//  ██╔══██╗██╔════╝██║   ██║██║     ██╔═══██╗██╔════╝      
//  ██║  ██║█████╗  ██║   ██║██║     ██║   ██║██║  ███╗     
//  ██║  ██║██╔══╝  ╚██╗ ██╔╝██║     ██║   ██║██║   ██║     
//  ██████╔╝███████╗ ╚████╔╝ ███████╗╚██████╔╝╚██████╔╝     
//  ╚═════╝ ╚══════╝  ╚═══╝  ╚══════╝ ╚═════╝  ╚═════╝      
//                                                          
```

## TODO


# REVISIONS
---
# v0.6.4
- [x] Update to core `0.6.4.1407`
- [x] IOs can now be named with `string` --> Add `int` override
- [x] Add `toolPin` option for IOs (UR robots)
- [x] Remove `Robot.SetIOName()`
- [x] `Speed/To` can now be a `double`
- [x] `Precision/To` can now be a `double`
- [x] Add `Acceleration` and `AccelerationTo` actions
- [x] Add `RotationSpeed/To()` option
- [x] Add `JointSpeed/To()` and `JointAcceleration/To()` for UR robots
- [x] Rename `Tool.Create` and `Robot.Create`
- [x] Update icons
- [x] Fix `ToolCreate` new creationbug: no need to restart Dynamo anymore
- [x] Verify sample files work --> loading geometry doesn't work anymore with DynamoRevit...
- [x] Add `SendToBridge()`

---
# REVISIONS
## v0.5.0
- [x] Update Machina core to 0.5.0
- [x] ICONS!! :)
- [x] Split nodes into classes
- [x] Core refactor updates
- [x] Make API names and descriptions consistent with other APIs
- [x] Update `migrations`
- [x] Add 3D printing Actions
- [x] Review sample files

## v.0.4.1
- [x] Add 3D Printing sample
- [x] Rename and migrate `Zone` to `Precision`

## v0.4.0
- [x] Improve sample files
- [x] Add Robot.SetIOName()
- [x] Add "human comments" option to compile component
- [x] Add IOs


IF ($(ConfigurationName)) == (Debug) GOTO END
cd $(ProjectDir)
copy /y $(OutDir)MachinaDynamo.dll  "D:\Dropbox\Jose Luis\code\MachinaDynamo\package\Machina\bin"
copy /y $(OutDir)MachinaDynamo.xml "D:\Dropbox\Jose Luis\code\MachinaDynamo\package\Machina\bin"
copy /y $(OutDir)MachinaDynamo.customization.dll "D:\Dropbox\Jose Luis\code\MachinaDynamo\package\Machina\bin"
:END

Severity	Code	Description	Project	File	Line	Suppression State
Error		The command "IF (Release) == (Debug) GOTO END
cd C:\ONEDrive\OneDrive - autodesk\Code\Machina-Dynamo\src\MachinaDynamo\
copy /y C:\ONEDrive\OneDrive - autodesk\Code\Machina-Dynamo\src\MachinaDynamo\bin\Release\MachinaDynamo.dll "C:\ONEDrive\OneDrive - autodesk\Code\Machina-Dynamo\package\Machina\bin"
:END" exited with code 1.	MachinaDynamo			
