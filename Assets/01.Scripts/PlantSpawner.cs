using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    private GameObject selectedPlantPrefab;
    [SerializeField] GameObject[] gameBoardPrefabs;
    private List<GameBoard> gameBoards = new List<GameBoard>();

    void Start()
    {
        foreach (GameObject boardPrefab in gameBoardPrefabs)
        {
            GameObject boardInstance = Instantiate(boardPrefab, transform.position, Quaternion.identity);
            gameBoards.Add(boardInstance.GetComponent<GameBoard>());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectedPlantPrefab != null)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.y = 0;

            GameBoard closestBoard = FindClosestBoard(clickPosition);

            if (closestBoard != null)
            {
                closestBoard.TryPlacePlant(selectedPlantPrefab, clickPosition);
            }
        }
    }

    private GameBoard FindClosestBoard(Vector3 position)
    {
        float closestDistance = Mathf.Infinity;
        GameBoard closestBoard = null;

        foreach (GameBoard board in gameBoards)
        {
            float distance = Vector3.Distance(position, board.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestBoard = board;
            }
        }

        return closestBoard;
    }

    public void SetSelectedPlant(GameObject plantPrefab)
    {
        selectedPlantPrefab = plantPrefab;
    }
}
