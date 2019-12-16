using System;
using System.Collections;
using System.Collections.Generic;
using Com.Ryuuguu.HexGridCC;
using UnityEngine;

/// <summary>
/// A Hex Board
/// handles dropping cards at  coords
/// converting mouse coord
/// highlighting card under mouse
///     depends on validity of drop 
/// </summary>
public class HexBoard : PhyloHexGrid {

   public FilteredCell filteredCell;

   // Start with an card at  zero 
   // place empty card around that can be dropped on 
   // change to empty card that cen be dropped on 


   private void Start() {
      Init();
      Test();
      
   }

   void Test() {
      AddCard("Marbled Murrelet", Vector3.zero);
      foreach (var dir in CubeCoordinates.CubeDirections) {
         var card = Instantiate(filteredCell, holder, true);
         AddCard(card, dir);
      }
   }
   
   public void AddCard(string speciesId, Vector3 coord) {
      var card =  Instantiate<SpeciesCardUI>((SpeciesCardUI)prefab, holder,true);
      AddCell(coord,card);
      card.SetupSpecies(speciesId);
   }

   public void AddCard(HexMBPhylo card, Vector3 coord) {
      AddCell(coord,card);
      
   }
   
}
