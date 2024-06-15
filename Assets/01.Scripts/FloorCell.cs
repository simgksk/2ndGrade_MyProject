using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCell : MonoBehaviour
{
    Vector2Int gridPosition;
    GameObject placedObj;

    void Initialize(Vector2Int position)
    {
        gridPosition = position;
    }

    public Vector3 GetCenterPosition()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.center;
        }

        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            return collider.bounds.center;
        }

        return transform.position;
    }

    public bool IsOccupied()
    {
        return placedObj != null;
    }

    public void PlaceObj(GameObject obj)
    {
        placedObj = obj;
    }
}
