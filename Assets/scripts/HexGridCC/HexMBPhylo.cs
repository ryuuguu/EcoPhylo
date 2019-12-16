using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMBPhylo : MonoBehaviour{
    public GameObject  highlight;
    
    
    
    
    // Hides the Hex
    public void Unhighlight() {
        highlight.SetActive(false);
    }

    // Shows the hex
    public void Highlight() {
        highlight.SetActive(true);
    }
    
    public void Highlight(bool val) {
        highlight.SetActive(val);
    }

    
}
