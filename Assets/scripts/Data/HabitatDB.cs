using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HabitatDB : MonoBehaviour{
    
    public Sprite outlineIcon;
    public List<Habitat> habitats;
    public List<ClimateData> climates;

}


[System.Serializable]
public class Habitat {
    public string id;
    public string name;
    public Color color;
    public Sprite  icon;

}

[System.Serializable]
public class ClimateData {
    [FormerlySerializedAs("climateId")] public string id;
    public string name;
    public Color color;
    public Sprite icon;
}