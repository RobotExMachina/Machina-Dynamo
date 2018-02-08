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
- [ ] Review problem with Tools getting lost on rewrite

---
# REVISIONS
## v.0.5.0
- [x] Update Machina core to 0.5.0
- [x] ICONS!! :)
- [~] Split nodes into classes

- [ ] Hide most pre-0.5.0 components
- [ ] Remove all obsolete components, and create new ones with GUID to avoid overwrite
- [ ] Make components have the option to choose between abs/rel
- [ ] Rename `Motion` to `MotionMode`
- [ ] Rename `FeedRate` to `ExtrusionRate`
- [ ] Add GH_MutableComponent middleware class
- [ ] Split component classes per file
- [ ] Generate new components with new GuiD
- [ ] Rename `CreateRobot` to `NewRobot` , same for `NewTool`
- [ ] Mutable components now accept default values
- [ ] Add namespaces to components
- [ ] Add `Temperature`
- [ ] Add `Extrude`
- [ ] Add `ExtrusionRate`
- [ ] Update sample files

## v.0.4.1
- [x] Add 3D Printing sample
- [x] Rename and migrate `Zone` to `Precision`

## v0.4.0
- [x] Improve sample files
- [x] Add Robot.SetIOName()
- [x] Add "human comments" option to compile component
- [x] Add IOs
