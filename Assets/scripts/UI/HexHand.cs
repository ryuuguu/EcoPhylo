using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexHand : HexGrid {
    public SpeciesCardUI prefabSpeciesCardUi;
    
    
    public void Start() {
        MakeGrid();
    }

    
    /// <summary>
    /// for now just put in first Hand position
    /// </summary>
    /// <param name="speciesId"></param>
    public void AddCard(string speciesId) {
        Instantiate<SpeciesCardUI>(prefabSpeciesCardUi, transform,true);
       
        // set location
        // call setup Species
    }
    
}
