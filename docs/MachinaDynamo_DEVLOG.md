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
## v0.5.0
- [x] Update Machina core to 0.5.0
- [x] ICONS!! :)
- [~] Make API names and descriptions consistent with other APIs
- [~] Split nodes into classes
- [x] Core refactor updates


### v0.5.0 NODEMODEL ATTEMPT...
Tried migrating the Nodes to NodeModels with potentially custom UI. The hope was to:
- Embed a dropdown for abs/rel actions.
- Mutate the Node's names and inputs dynamically to adapt to abs/rel mode.
- Add an icon on the Node face itself.

A basic NodeModel component is quite simple to do. However, I ran into a bunch of side problems:
- Implementing the dropdown is a pain in the ass: needs either a WPF (too manual) or dynamic generation of a ComboBox (too painful), plus managing all the bstract dropdown elements, their serialization... Very long and tedious. Dynamo comes with a base class to inherit from, but found it hard to use if I was going to further customize the Node face and the mutability. Refs: https://github.com/DynamoDS/Dynamo/blob/master/src/Libraries/CoreNodeModels/DropDown.cs, https://github.com/DynamoDS/Dynamo/blob/master/src/Libraries/CoreNodeModelsWpf/NodeViewCustomizations/DSDropDownBase.cs.
- I couldn't figure out how to implement icons programmatically. There is something I am not quite getting about accessing resources dynamically from the code.
- The assembly structure needs to change completely. A dll cannot contain Zero Touch and NodeModels together. So either all ZT Nodes migrate to NodeModels (which would need to be done if they are to have icons), or split the dll in two families. 
- Furthermore, the AST builder needs references to static delegates in a different assembly (Machina core), but then, if loaded as `node_libraries` in `pkg.json`, the core library shows up in Dynamo! Nono suggests a middleware DLL to pass calls between MachinaDynamo.dll and the core, which is yet another overkill but also strictly necessary to perform conversions between Dynamo geometry types and Machina types (and keep Machina core agnostic to other platforms).
- I am honestly not very sure how would I mutate the Node's inputs and change the AST dynamically, especially given that it looks like it needs to be very _abstractly precanned_ before execution...

Therefore after a couple tests, I am coing to opt for keeping the ZT structure, maintaining separate ./To components that mirror the core API, and move on to developing the real-time part... 

Some usefu links:
https://github.com/DynamoDS/Dynamo/wiki/How-To-Create-Your-Own-Nodes
https://github.com/teocomi/HelloDynamo
http://teocomi.com/dynamo-unchained-2-learn-how-to-develop-explicit-nodes-in-csharp/



---
## v.0.4.1
- [x] Add 3D Printing sample
- [x] Rename and migrate `Zone` to `Precision`

---
## v0.4.0
- [x] Improve sample files
- [x] Add Robot.SetIOName()
- [x] Add "human comments" option to compile component
- [x] Add IOs
