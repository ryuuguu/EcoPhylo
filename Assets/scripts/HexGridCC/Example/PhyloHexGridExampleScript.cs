using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Com.Ryuuguu.HexGridCC;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PhyloHexGridExampleScript : PhyloHexGrid {

    public int exampleRadius = 8;
    
    public PointerTransform pointerTransform;
    
    public Text displayTime1;
    //[FormerlySerializedAs("displayDelta")] 
    public Text displayTime2;
    public Text displayHexes;
    
    protected void Start() {
        Init();
        var coordList = cubeCoordinates.Construct(exampleRadius);
        MakeAllHexes(localSpaceId);
        NewMap();
    }
    
    void Update() {
        MovePointer();
        if (Input.GetKeyDown(KeyCode.Return)) {
            NewMap();
            return;
        }
        if (cubeCoordinates.GetCoordinatesFromContainer(AllToken).Count == 0)
            return;

        if (Input.GetKeyDown(KeyCode.C))
        {
           // _cubeCoordinates.ShowCoordinatesInContainer(AllToken);
        }

        if (Input.GetKeyDown(KeyCode.L))
            ShowExample(localSpaceId,"line");

        if (Input.GetKeyDown(KeyCode.R))
            ShowExample(localSpaceId,"reachable");

        if (Input.GetKeyDown(KeyCode.S))
            ShowExample(localSpaceId,"spiral");

        if (Input.GetKeyDown(KeyCode.P))
            ShowExample(localSpaceId,"path");
    }
    

   
    
    void MovePointer() {        
        if (Input.GetMouseButton(0)) {
            var hexCenteredPos = MouseHexCenteredPos();
            pointerTransform.ShowPointer(hexCenteredPos,true);
        }
    }

    
    void NewMap() {
        
        DestroyAllHexes(localSpaceId);
        Timer.StartTimer();
        var gameScale = displayScale * hexScale2Radius * hexScale;
        localSpaceId =  CubeCoordinates.NewLocalSpaceId(gameScale, orientation, holder);
        cubeCoordinates.Construct(exampleRadius);

        // Remove 25% of Coordinates except 0,0,0
        foreach (Vector3 cube in cubeCoordinates.GetCubesFromContainer(AllToken)) {
            if (cube == Vector3.zero)
                continue;

            if (Random.Range(0.0f, 100.0f) < 25.0f)
                cubeCoordinates.RemoveCube(cube);
        }

        // Remove Coordinates not reachable from 0,0,0
        cubeCoordinates.RemoveCubes(
            cubeCoordinates.BooleanDifferenceCubes(
                cubeCoordinates.GetCubesFromContainer(AllToken),
                cubeCoordinates.GetReachableCubes(Vector3.zero, exampleRadius )
            )
        );
        
        displayTime1.text = Timer.CalcTimer().ToString();
        Timer.StartTimer();
        MakeAllHexes(localSpaceId);
        
        displayTime2.text = Timer.CalcTimer().ToString();
        displayHexes.text = cubeCoordinates.GetCoordinatesFromContainer(AllToken).Count.ToString();
        
        // Construct Examples
        ConstructExamples();
    }

    private void ConstructExamples() {
        List<Vector3> allCubes = cubeCoordinates.GetCubesFromContainer(AllToken);
        // Line between the first and last cube coordinate
        var line = cubeCoordinates.GetLineBetweenTwoCubes(allCubes[0], allCubes[allCubes.Count - 1]);
        cubeCoordinates.AddCubesToContainer(line , "line");
        
        // Path between the first and last cube coordinate
        cubeCoordinates.AddCubesToContainer(cubeCoordinates.GetPathBetweenTwoCubes(allCubes[0], allCubes[allCubes.Count - 1]), "path");
        
        // Reachable, 3 coordinates away from 0.0.0
        cubeCoordinates.AddCubesToContainer(cubeCoordinates.GetReachableCubes(Vector3.zero, exampleRadius/3), "reachable");
 
        // Spiral, 3 coordinates away from 0.0.0
        cubeCoordinates.AddCubesToContainer(cubeCoordinates.GetSpiralCubes(Vector3.zero, exampleRadius/3), "spiral");
        
    }

    private void ShowExample(string aLocalSpaceId, string containerId) {
        
        var allCoords = cubeCoordinates.GetCoordinatesFromContainer(AllToken);
        foreach (var coord in allCoords) {
            hexes[aLocalSpaceId][coord.cubeCoord].Unhighlight();
             
        }
        
        var exampleCoords = cubeCoordinates.GetCoordinatesFromContainer(containerId);
        foreach (var coord in exampleCoords) {
            hexes[aLocalSpaceId][coord.cubeCoord].Highlight();
        }
    }
    
    private void DebugCoords(string msg, string containerName) {
        var allCoords = cubeCoordinates.GetCoordinatesFromContainer(containerName);
        Debug.Log("=============== " + msg );
        foreach (var coord in allCoords) {
            Debug.Log( msg+ " : " + containerName+ " : " +coord.cubeCoord);
        }
    }

}
