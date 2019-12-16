using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HexGrid : MonoBehaviour {

    public int width = 6;
    public int height = 6;
    public Vector2 offset = Vector2.zero;
    
    public HexCell cellPrefab;

    public float scale = 40;
    
    public List<HexCell> cells;


    public void ClearGrid() {
        var cells = GetComponentsInChildren<HexCell>();
        for (int i = 0; i < cells.Length; i++) {
            Destroy(cells[i].gameObject);
        }
    }
    
    public void MakeGrid() {
        ClearGrid();
        cells = new List<HexCell>();

        int z = 0;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                var v3 = new Vector3Int(x,y,z);
                CreateCell(v3,offset);
            }
        }
    }

    public void CreateCell(Vector3Int v3,Vector2 anOffset) {
        HexCell cell = Instantiate<HexCell>(cellPrefab);
        cells.Add(cell);
        SetCellPosition(cell, v3, anOffset);
    }

    public void SetCellPosition(HexCell cell, Vector3Int v3, Vector2 anOffset) {
        Vector3 pos = new Vector3((anOffset.x+v3.x+v3.y*0.5f- v3.y / 2) * cell.outerRadius * HexCell.innerOuterRatio * 2f,
            (anOffset.y * cell.outerRadius*2f)+v3.y * cell.outerRadius*1.5f,
            
            0);
        var tran = cell.transform;
        tran.SetParent(transform, false);
        tran.localPosition = pos*scale;
        tran.localScale = cell.transform.localScale*scale;
        cell.SetLocation(v3);
    }
    
}
        
    
