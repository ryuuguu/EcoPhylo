using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesDB : MonoBehaviour {

    
    public Sprite foodChainBackground;

    public HabitatDB habitatDb;
    public List<FoodChainData> foodChainDatas;
    public List<Species> specieses;


    public static SpeciesDB inst;
    
    public void Awake() {
        inst = this;
    }

    public static FoodChainData FoodChainData(string aFoodChainID) {
       return inst.foodChainDatas.Find((data => data.foodChainId == aFoodChainID));
    }
    
    public static Species Species(string anId) {
        return inst.specieses.Find((data => data.id == anId));
    }

    public static Habitat Habitat(string anId) {
        return inst.habitatDb.habitats.Find((data => data.id == anId));
    }

    public static ClimateData ClimateData(string anId) {
        return inst.habitatDb.climates.Find((data => data.id == anId));
    }


    
    
    #region Inspector helpers
    
    public static string[] HabitatIds() {
        var speciesDb = GameObject.FindObjectOfType<SpeciesDB>();
        var temp = new List<string>();
        foreach (var habitat in speciesDb.habitatDb.habitats) {
            temp.Add(habitat.id);
        }
        return temp.ToArray();
    }
    
    public static string[] ClimateIds() {
        var speciesDb = GameObject.FindObjectOfType<SpeciesDB>();
        var temp = new List<string>();
        foreach (var climate in speciesDb.habitatDb.climates) {
            temp.Add(climate.id);
        }
        return temp.ToArray();
    }
    
    public static string[] FoodChainIds() {
        var speciesDb = GameObject.FindObjectOfType<SpeciesDB>();
        var temp = new List<string>();
        foreach (var foodChainData in speciesDb.foodChainDatas) {
            temp.Add(foodChainData.foodChainId);
        }
        return temp.ToArray();
    }
    
    #endregion
    
}

[System.Serializable]
public class Species  {
    public string id;
    public string speciesName;
    [DropDownList(typeof(SpeciesDB), "HabitatIds")]
    public List<string> habitatIds;
    [DropDownList(typeof(SpeciesDB), "FoodChainIds")]
    public string foodChainId;
    [DropDownList(typeof(SpeciesDB), "ClimateIds")]
    public List<string>  climateIds;
    public int size;
    public int pointHandicap;
    public string note;
    //should be calculated 
    public int pointValue;

}

[System.Serializable]
public class FoodChainData {
    public string foodChainId;
    public string name;
    public List<string> foodIds;
    public Color color;
    public Sprite icon;
    public float score;
}

