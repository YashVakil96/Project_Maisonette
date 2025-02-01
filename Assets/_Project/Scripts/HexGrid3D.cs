using UnityEngine;
using System.Collections.Generic;

public class HexGrid3D : MonoBehaviour
{
    public GameObject hexPrefab;
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float hexWidth = 1.0f;
    public float hexHeight = 1.0f;

    public List<Vector3> tilePositions = new List<Vector3>(); // Store hex tile positions

    void Start()
    {
        GenerateGrid();
        ItemSpawner.Instance.InitItemSpawner();
    }

    void GenerateGrid()
    {
        tilePositions.Clear(); // Clear existing positions
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float xPos = x * hexWidth;
                if (y % 2 == 1)
                {
                    xPos += hexWidth / 2;
                }
                Vector3 position = new Vector3(xPos, 0, y * hexHeight * 0.75f);
                tilePositions.Add(position); // Add position to the list
                GameObject hex = Instantiate(hexPrefab, position, Quaternion.Euler(90,0,90));
                hex.transform.parent = this.transform;
                hex.name = $"Hex_{x}_{y}";
            }
        }
    }
}