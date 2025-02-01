using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GameObject hexPrefab; // Assign a 3D hex tile prefab
    public int gridSize = 3; // Radius of hexagonal shape
    public float hexRadius = 1f; // Size of hexes
    public List<Vector3> tilePositions = new List<Vector3>(); // Store hex tile positions

    void Start()
    {
        GenerateHexGrid();
        ItemSpawner.Instance.InitItemSpawner();
    }

    void GenerateHexGrid()
    {
        tilePositions.Clear(); // Clear existing positions
        List<Vector2Int> hexCoordinates = new List<Vector2Int>();

        // Generate hexagonal shape using axial coordinates
        for (int q = -gridSize; q <= gridSize; q++)
        {
            int r1 = Mathf.Max(-gridSize, -q - gridSize);
            int r2 = Mathf.Min(gridSize, -q + gridSize);

            for (int r = r1; r <= r2; r++)
            {
                hexCoordinates.Add(new Vector2Int(q, r));
            }
        }

        // Convert coordinates to 3D world positions and instantiate hex tiles
        foreach (Vector2Int hex in hexCoordinates)
        {
            Vector3 position = HexToWorld(hex.x, hex.y);
            Instantiate(hexPrefab, position, Quaternion.Euler(90, 0, 90), transform);
            tilePositions.Add(position); // Add position to the list
        }
    }

    Vector3 HexToWorld(int q, int r)
    {
        float xOffset = Mathf.Sqrt(3) * hexRadius;
        float zOffset = 1.5f * hexRadius;

        float x = (q * xOffset) + (r * xOffset / 2);
        float z = r * zOffset;

        return new Vector3(x, 0, z); // Using Y=0 for ground placement
    }
}