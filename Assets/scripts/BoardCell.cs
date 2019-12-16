using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell  {

    public BoardCell anchorCell;
    public Vector2Int cellPos;
    public Card card;
    public bool[] edgeHighlights = new bool[4] {false,false,false,false};
    
}

