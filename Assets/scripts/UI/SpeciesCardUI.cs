using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SpeciesCardUI : CardUI {

    public GameObject speciesDisplay;

    public Species species;

    public Image foodChainIcon;
    public Image[] habitatIcons;
    public Image[] climateIcons;
    public Image speciesImage; //not used yet
    public TMPro.TMP_Text speciesName;
    public TMPro.TMP_Text sizeText;
    public TMPro.TMP_Text pointText;
    //Used for detailed view
    public TMPro.TMP_Text scienceName; //not used yet
    public TMPro.TMP_Text note; //not used yet


    // needs to access
    //   SpeciesDB
    //        get foodChainIcon
    //     habitate icons?
    //     get climate icons
    // 
    public void SetupSpecies(string anId) {
        species = SpeciesDB.Species(anId);
        var fcd = SpeciesDB.FoodChainData(species.foodChainId);
        foodChainIcon.sprite = fcd.icon;
        foodChainIcon.color = fcd.color;
        for (int i = 0; i < species.habitatIds.Count; i++) {
            var habitat = SpeciesDB.Habitat(species.habitatIds[i]);
            habitatIcons[i].sprite = habitat.icon;
            habitatIcons[i].color = habitat.color;
        }
        for (int i = 0; i < species.climateIds.Count; i++) {
            var climate = SpeciesDB.ClimateData(species.climateIds[i]);
            climateIcons[i].sprite = climate.icon;
            climateIcons[i].color = climate.color;
        }

        speciesName.text = species.speciesName;
        sizeText.text = species.size.ToString();
        pointText.text = species.pointValue.ToString();


    }



}
