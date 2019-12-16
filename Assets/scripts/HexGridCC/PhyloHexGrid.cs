using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Com.Ryuuguu.HexGridCC;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PhyloHexGrid : MonoBehaviour {
    public RectTransform holder;
    public HexMBPhylo prefab;
   
    public CubeCoordinates.LocalSpace.Orientation orientation;

   
    public float displayScale = 2;
    public float hexScale2Radius = 100f;
    public float hexScale = 1;
    public Vector2 offsetCoord = Vector3.zero;
    
    
    public string localSpaceId;

    protected string AllToken;
    
    public CubeCoordinates cubeCoordinates;

    public Dictionary<string,Dictionary<Vector3, HexMBPhylo>> hexes = new Dictionary<string,Dictionary<Vector3, HexMBPhylo>>();

    protected void Init() {
        cubeCoordinates = new CubeCoordinates();
        AllToken = CubeCoordinates.AllContainer;
        var gameScale = displayScale * hexScale2Radius * hexScale;
        localSpaceId = CubeCoordinates.NewLocalSpaceId(gameScale, orientation, holder,offsetCoord);
        hexes[localSpaceId] = new Dictionary<Vector3, HexMBPhylo>();
    }


    public HexMBPhylo AddCell(Vector3 coord, HexMBPhylo cell) {
        HexMBPhylo result = null;
        var ls = CubeCoordinates.GetLocalSpace(localSpaceId);
        cubeCoordinates.AddCube(coord);
        var localCoord = CubeCoordinates.ConvertPlaneToLocalPosition(coord , ls);
        if (hexes[localSpaceId].ContainsKey((coord))) {
            result = hexes[localSpaceId][coord];
        }
        hexes[localSpaceId][coord] = cell;

        var transform1 = cell.transform;
        transform1.localPosition = localCoord;
        transform1.localScale = hexScale * displayScale * Vector3.one;
        
        return result;
    }
    
    public HexMBPhylo RemoveCell(Vector3 coord) {
        HexMBPhylo result = null;
        cubeCoordinates.RemoveCube(coord);
        if (hexes[localSpaceId].ContainsKey((coord))) {
            result = hexes[localSpaceId][coord];
            
        }
        return result;
    }
    
    public void MakeAllHexes(string aLocalSpaceId) {
        var allCoords = cubeCoordinates.GetCoordinatesFromContainer(AllToken);
        var ls = CubeCoordinates.GetLocalSpace(localSpaceId);
        if (!hexes.ContainsKey(aLocalSpaceId)) {
            hexes[aLocalSpaceId] = new Dictionary<Vector3, HexMBPhylo>();
        }
        if (ls.spaceRectTransform != null) {
            foreach (var coord in allCoords) {
                var localCoord = CubeCoordinates.ConvertPlaneToLocalPosition(coord.cubeCoord, ls);
                var hex = Instantiate(prefab, ls.spaceRectTransform);
                var tran = hex.transform;
                tran.localPosition = localCoord;
                tran.localScale = hexScale * displayScale * Vector3.one;
                hexes[aLocalSpaceId][coord.cubeCoord] = hex;
                hex.name += coord.cubeCoord;
            }
        }
    }
    
    public void DestroyAllHexes(string aLocalSpaceId) {
       
        foreach (var hex in hexes[aLocalSpaceId].Values) {
            Destroy(hex.gameObject);
        }
        hexes[aLocalSpaceId].Clear();

    }
    
    protected Vector3 MouseHexCenteredPos() {
        var mouseCoord = MouseCoord();
        var hexCenteredPos =
            CubeCoordinates.ConvertPlaneToLocalPosition(mouseCoord, localSpaceId);
        return hexCenteredPos;
    }

    protected Vector2 MouseCoord() {
        // convert mouse to local rectTransform 
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) transform, Input.mousePosition, null,
            out var localPoint);
        // convert rectTrans to plane coord  & round
        var mouseCoord = CubeCoordinates.ConvertLocalPositionToPlane(localPoint, localSpaceId);
        //convert backTrans 
        return mouseCoord;
    }


}
