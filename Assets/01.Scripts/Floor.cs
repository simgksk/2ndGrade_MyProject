using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject[,] cells;
    public Vector2Int floorSize;
    public Vector2 cellSize;

    void Start()
    {
        cells = new GameObject[floorSize.x, floorSize.y];
        foreach(Transform cell in transform)
        {
            FloorCell floorCell = cell.GetComponent<FloorCell>();

            cells[floorCell.gridPosition.x, floorCell.gridPosition.y] = cell.gameObject;
        }

        if (cells[0, 0] != null)
        {
            Renderer renderer = cells[0, 0].GetComponent<Renderer>();
            if (renderer != null)
            {
                cellSize = renderer.bounds.size;
            }
        }
    }

    public Vector3 GetCellCenterPosition(Vector2Int gridPos)
    {
        GameObject cell = cells[gridPos.x, gridPos.y];

        if (cell != null)
        {
            Renderer renderer = cell.GetComponent<Renderer>();
            if(renderer != null)
            {
                return renderer.bounds.center;
            }
        }
        return Vector3.zero;
    }
}
