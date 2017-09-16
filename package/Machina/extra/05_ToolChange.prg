MODULE BRobotProgram

  CONST speeddata vel100 := [100,100,5000,1000];

  PERS tooldata marker := [TRUE, [[0,0,150],[1, 0, 0, 0]], [1,[0, 0, 75],[1,0,0,0],0,0,0]];
  CONST zonedata zone3 := [FALSE,3,4.5,4.5,0.45,4.5,0.45];

  PROC main()
    ConfJ \Off;
    ConfL \Off;

    MoveJ [[300, -300, 300], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z5, Tool0\WObj:=WObj0;  ! [3012]
    MoveL [[300, -300, 150], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, zone3, Tool0\WObj:=WObj0;  ! [3003]
    ! Tool "marker" attached
    WaitTime 2.5;  ! [2998]
    MoveL [[300, -300, 300], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, zone3, marker\WObj:=WObj0;  ! [3006]
    TPWrite "Now, let's draw!";  ! [2981]
    MoveJ [[400, 300, 75], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z5, marker\WObj:=WObj0;  ! [2997]
    MoveL [[400, 300, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2983]
    MoveL [[366.667, 300, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2984]
    MoveL [[333.333, 300, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2985]
    MoveL [[300, 300, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2986]
    MoveL [[330, 316.667, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2987]
    MoveL [[360, 333.333, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2988]
    MoveL [[390, 350, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2989]
    MoveL [[360, 366.667, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2990]
    MoveL [[330, 383.333, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2991]
    MoveL [[300, 400, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2992]
    MoveL [[333.333, 400, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2993]
    MoveL [[366.667, 400, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2994]
    MoveL [[400, 400, 50], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2995]
    MoveL [[400, 400, 75], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z1, marker\WObj:=WObj0;  ! [2996]
    WaitTime 1;  ! [2975]
    TPWrite "Housekeeping: put the tool back in place";  ! [2968]
    MoveL [[300, -300, 300], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z5, marker\WObj:=WObj0;  ! [3005]
    MoveL [[300, -300, 0], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, zone3, marker\WObj:=WObj0;  ! [3002]
    ! Tool detached
    WaitTime 2.5;  ! [2970]
    MoveL [[300, -300, 300], [0, 0, 1, 0], [0,0,0,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, zone3, Tool0\WObj:=WObj0;  ! [3004]
    MoveAbsJ [[0,0,0,0,90,0], [0,9E9,9E9,9E9,9E9,9E9]], vel100, z5, Tool0\WObj:=WObj0;  ! [3009]

  ENDPROC

ENDMODULE
