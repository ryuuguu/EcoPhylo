using System.Collections;
using System.Collections.Generic;
using Com.Ryuuguu.HexGridCC;
using UnityEngine;

public class HexHandCC : PhyloHexGrid {

    public Vector3 nextDirection =  new Vector3(0,-1,1);

    public int currentHandSize = 0;
    public int maxHandSize = 9;// not used yet
    
    public void Start() {
        Init();
        Test();
    }

    public void Test() {
       AddCard("Marbled Murrelet"); 
       AddCard("Dummy"); 
       AddCard("Marbled Murrelet"); 
       AddCard("Dummy"); 
       AddCard("Marbled Murrelet"); 
       AddCard("Dummy"); 
       AddCard("Marbled Murrelet"); 
       AddCard("Dummy"); 
       AddCard("Marbled Murrelet");
       RemoveCard(3);
    }

    
    /// <summary>
    /// for now just put in first Hand position
    /// </summary>
    /// <param name="speciesId"></param>
    /// <param name="coord"></param>
    public void AddCard(string speciesId) {
        AddCard(speciesId, currentHandSize);
    }

    public void AddCard(string speciesId, int index) {
        var coord = nextDirection * index;
        var card =  Instantiate<SpeciesCardUI>((SpeciesCardUI)prefab, holder,true);
        AddCell(coord,card);
        card.SetupSpecies(speciesId);
        currentHandSize++;
    }
    
    public void RemoveCard(int index) {
        var cell = RemoveCell(nextDirection * index);
        Destroy(cell.gameObject);
        ShiftCardsDown(index);
    }

    public void ShiftCardsDown(int index) {
        for (int i = index + 1; i < currentHandSize; i++) {
            var coord = nextDirection * i;
            var cell =  RemoveCell(coord);
            AddCell(nextDirection * (i-1),cell);
        }
    }

}
