MODULE BRobotProgram

  CONST speeddata vel20 := [20,20,5000,1000];
  CONST speeddata vel200 := [200,200,5000,1000];
  CONST speeddata vel75 := [75,75,5000,1000];
  CONST speeddata vel25 := [25,25,5000,1000];

  PERS tooldata MillingBit := [TRUE, [[0,0,100],[1, 0, 0, 0]], [1,[0, 0, 50],[1,0,0,0],0,0,0]];

  CONST robtarget target3 := [[194.824, 46.75, 354.326], [0.051916, -0.001689, 0.998122, 0.032472], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target5 := [[200, 50, 304.701], [0.051916, -0.001689, 0.998122, 0.032472], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target7 := [[249.378, 50, 310.944], [0.072588, -0.012066, 0.98379, 0.163535], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target8 := [[299.171, 50, 319.416], [0.093019, -0.017031, 0.97924, 0.179294], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target9 := [[349.378, 50, 330.115], [0.114432, -0.009638, 0.98988, 0.083371], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target10 := [[400, 50, 343.043], [0.133238, 0.017352, 0.982634, -0.127972], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target12 := [[387.13, 62.806, 389.631], [0.133238, 0.017352, 0.982634, -0.127972], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target16 := [[181.115, 95.495, 355.116], [0.193213, -0.009671, 0.979882, 0.049049], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target18 := [[200, 100.488, 309.089], [0.193213, -0.009671, 0.979882, 0.049049], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target20 := [[249.378, 99.361, 325.57], [0.128293, -0.015126, 0.9848, 0.11611], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target21 := [[299.171, 98.876, 334.976], [0.058342, -0.007188, 0.99078, 0.122065], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target22 := [[349.378, 99.034, 337.306], [-0.012517, 0.000744, 0.998158, 0.059358], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target23 := [[400, 99.833, 332.561], [-0.078914, -0.005658, 0.994313, -0.071295], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target25 := [[407.806, 106.967, 381.43], [-0.078914, -0.005658, 0.994313, -0.071295], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target29 := [[177.053, 143.973, 359.539], [0.237883, -0.015956, 0.968985, 0.064993], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target31 := [[200, 150.65, 315.62], [0.237883, -0.015956, 0.968985, 0.064993], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target33 := [[249.378, 149.148, 335.472], [0.141108, -0.01016, 0.987386, 0.071096], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target34 := [[299.171, 148.502, 344.232], [0.032302, -0.001961, 0.99764, 0.060555], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target35 := [[349.378, 148.712, 341.901], [-0.077782, 0.002531, 0.99644, 0.032429], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target36 := [[400, 149.778, 328.478], [-0.17741, -0.001466, 0.984102, -0.00813], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target38 := [[417.458, 150.604, 375.324], [-0.17741, -0.001466, 0.984102, -0.00813], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target42 := [[180.245, 191.646, 369.366], [0.204257, -0.018203, 0.974884, 0.08688], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target44 := [[200, 200.488, 324.293], [0.204257, -0.018203, 0.974884, 0.08688], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target46 := [[249.378, 199.361, 340.65], [0.113576, -0.003087, 0.993158, 0.026992], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target47 := [[299.171, 198.876, 347.186], [0.016247, 3.2E-05, 0.999866, -0.001951], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target48 := [[349.378, 199.034, 343.902], [-0.08054, 0.000552, 0.996728, 0.006828], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target49 := [[400, 199.833, 330.797], [-0.170315, 0.00881, 0.984035, 0.0509], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target51 := [[416.715, 194.675, 377.637], [-0.170315, 0.00881, 0.984035, 0.0509], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target55 := [[192.546, 238.422, 383.175], [0.076327, -0.008959, 0.990245, 0.116226], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target57 := [[200, 250, 335.109], [0.076327, -0.008959, 0.990245, 0.116226], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target59 := [[249.378, 250, 341.103], [0.043867, 0.00082, 0.998863, -0.018665], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target60 := [[299.171, 250, 343.836], [0.010996, 0.000691, 0.99797, -0.06273], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target61 := [[349.378, 250, 343.307], [-0.021401, -0.000394, 0.999601, -0.018414], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target62 := [[400, 250, 339.517], [-0.052748, 0.00614, 0.991892, 0.115459], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST robtarget target64 := [[405.161, 238.515, 387.905], [-0.052748, 0.00614, 0.991892, 0.115459], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]];
  CONST jointtarget target66 := [[0,0,0,0,90,0], [0,9E9,9E9,9E9,9E9,9E9]];

  PROC main()
    ConfJ \Off;
    ConfL \Off;

    ! Tool "MillingBit" attached
    TPWrite "Starting Path #0";
    MoveL target3, vel200, z5, MillingBit\WObj:=WObj0;  ! [62]
    MoveL target5, vel75, z5, MillingBit\WObj:=WObj0;  ! [57]
    MoveL target7, vel25, z5, MillingBit\WObj:=WObj0;  ! [32]
    MoveL target8, vel25, z5, MillingBit\WObj:=WObj0;  ! [33]
    MoveL target9, vel25, z5, MillingBit\WObj:=WObj0;  ! [34]
    MoveL target10, vel25, z5, MillingBit\WObj:=WObj0;  ! [35]
    MoveL target12, vel75, z5, MillingBit\WObj:=WObj0;  ! [52]
    WaitTime 2.5;
    TPWrite "Starting Path #1";
    MoveL target16, vel200, z5, MillingBit\WObj:=WObj0;  ! [63]
    MoveL target18, vel75, z5, MillingBit\WObj:=WObj0;  ! [58]
    MoveL target20, vel25, z5, MillingBit\WObj:=WObj0;  ! [36]
    MoveL target21, vel25, z5, MillingBit\WObj:=WObj0;  ! [37]
    MoveL target22, vel25, z5, MillingBit\WObj:=WObj0;  ! [38]
    MoveL target23, vel25, z5, MillingBit\WObj:=WObj0;  ! [39]
    MoveL target25, vel75, z5, MillingBit\WObj:=WObj0;  ! [53]
    WaitTime 2.5;
    TPWrite "Starting Path #2";
    MoveL target29, vel200, z5, MillingBit\WObj:=WObj0;  ! [64]
    MoveL target31, vel75, z5, MillingBit\WObj:=WObj0;  ! [59]
    MoveL target33, vel25, z5, MillingBit\WObj:=WObj0;  ! [40]
    MoveL target34, vel25, z5, MillingBit\WObj:=WObj0;  ! [41]
    MoveL target35, vel25, z5, MillingBit\WObj:=WObj0;  ! [42]
    MoveL target36, vel25, z5, MillingBit\WObj:=WObj0;  ! [43]
    MoveL target38, vel75, z5, MillingBit\WObj:=WObj0;  ! [54]
    WaitTime 2.5;
    TPWrite "Starting Path #3";
    MoveL target42, vel200, z5, MillingBit\WObj:=WObj0;  ! [65]
    MoveL target44, vel75, z5, MillingBit\WObj:=WObj0;  ! [60]
    MoveL target46, vel25, z5, MillingBit\WObj:=WObj0;  ! [44]
    MoveL target47, vel25, z5, MillingBit\WObj:=WObj0;  ! [45]
    MoveL target48, vel25, z5, MillingBit\WObj:=WObj0;  ! [46]
    MoveL target49, vel25, z5, MillingBit\WObj:=WObj0;  ! [47]
    MoveL target51, vel75, z5, MillingBit\WObj:=WObj0;  ! [55]
    WaitTime 2.5;
    TPWrite "Starting Path #4";
    MoveL target55, vel200, z5, MillingBit\WObj:=WObj0;  ! [66]
    MoveL target57, vel75, z5, MillingBit\WObj:=WObj0;  ! [61]
    MoveL target59, vel25, z5, MillingBit\WObj:=WObj0;  ! [48]
    MoveL target60, vel25, z5, MillingBit\WObj:=WObj0;  ! [49]
    MoveL target61, vel25, z5, MillingBit\WObj:=WObj0;  ! [50]
    MoveL target62, vel25, z5, MillingBit\WObj:=WObj0;  ! [51]
    MoveL target64, vel75, z5, MillingBit\WObj:=WObj0;  ! [56]
    WaitTime 2.5;
    MoveAbsJ target66, vel75, z5, MillingBit\WObj:=WObj0;  ! [1]

  ENDPROC

ENDMODULE
