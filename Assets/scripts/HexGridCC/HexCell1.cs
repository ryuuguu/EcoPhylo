using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HexCell  : MonoBehaviour {
    public const float innerOuterRatio = 0.866025404f;
    
    public Text positionText;
    public Vector3Int location;
    public Vector3Int cubeCoord;
    public float outerRadius = 2f;
    // this is a useful formula
    //innerRadius = outerRadius *innerOuterRatio;
    
    
    public void SetLocation(Vector3Int v3) {
        location = v3;
        name += v3.ToString();
        cubeCoord = new Vector3Int(v3.x-v3.y/2,v3.y,-1*((v3.x-v3.y/2)+v3.y));
        positionText.text = cubeCoord.ToString();
    }

}
