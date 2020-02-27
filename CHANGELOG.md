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
```

# WISHLIST

# TODO
## CHANGES IN GH v0.8.12
- [ ] Change `Compile` to spit out a `RobotProgram` instance
- [ ] Change `Display` to render it as a `List<string>`
- [ ] Add `Save` to write the program to a file somewhere on the system


# REVISIONS
## v0.8.8
- Fix `Connect` log typo
- Core update

## v0.8.6
- Core update to `v0.8.6`.
- [x] Deprecate all the weird joint Actions
- [x] Add `Acceleration`
- [x] Add `ArmAngle`
- [x] Add `DefineTool`
- [x] Update `Attach` and `Detach`
- [x] Icons for `WriteDigital/Analog`
- [x] Add `ExternalAxis`

- [x] Add `Bridge` section
- [x] Add `Bridge.Connect`
- [x] Rewrite `Bridge.Send`

- [x] Add `Bridge.Listen` --> Outputs all the messages received since last update.
- [x] Add listener parsers:
  - [x] `ActionExecuted`
  - [x] `ActionIssued`
  - [x] `ActionReleased`
  - [x] `MotionUpdate`

- [x] Add `Robot.Logger`

- [x] Icon update

- [x] Update sample files

- [x] Dist + release
- [x] Package manager

---
## v0.6.4

_(lost previous history...)_