using UnityEngine;

public class GameBoard : MonoBehaviour
{
    int rows = 5;
    int columns = 9;
    float cellSize = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        for (int i = 0; i <= rows; i++)
        {
            Gizmos.DrawLine(new Vector3(0, 0, i * cellSize), new Vector3(columns * cellSize, 0, i * cellSize));
        }
        for (int j = 0; j <= columns; j++)
        {
            Gizmos.DrawLine(new Vector3(j * cellSize, 0, 0), new Vector3(j * cellSize, 0, rows * cellSize));
        }
    }

    public Vector3 GetCellCenter(int row, int column)
    {
        float x = column * cellSize + cellSize / 2;
        float z = row * cellSize + cellSize / 2;
        return new Vector3(x, 0, z);
    }

    public bool TryPlacePlant(GameObject plantPrefab, Vector3 worldPosition)
    {
        int row = Mathf.FloorToInt((worldPosition.z - transform.position.z) / cellSize);
        int column = Mathf.FloorToInt((worldPosition.x - transform.position.x) / cellSize);

        if (row >= 0 && row < rows && column >= 0 && column < columns)
        {
            Vector3 position = GetCellCenter(row, column) + transform.position;
            Instantiate(plantPrefab, position, Quaternion.identity);
            return true;
        }
        return false;
    }
}
