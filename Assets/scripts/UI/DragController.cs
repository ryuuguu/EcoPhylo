using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour {


    public static  DragController inst;
    private static GameObject draggedObject;
    
    //event dragging 
    //dropping 
    

    private void Awake() {
        inst = this;
    }

}
