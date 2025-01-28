using System.Collections.Generic;
using UnityEngine;

public class HexGrid3D : MonoBehaviour
{
    #region New Code

    public GameObject hexPrefab;
    public int gridWidth = 5;
    public int gridHeight = 5;
    public float hexWidth = 1.0f;
    public float hexHeight = 1.0f;
    public List<Vector3> tilePositions = new List<Vector3>();


    void Start()
    {
        GenerateGrid();
        ItemSpawner.Instance.InitItemSpawner();
    }

    void GenerateGrid()
    {
        tilePositions.Clear();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float xPos = x * hexWidth;
                if (y % 2 == 1)
                {
                    xPos += hexWidth / 2;
                }

                GameObject hex = Instantiate(hexPrefab, new Vector3(xPos, 0, y * hexHeight * 0.75f),
                    Quaternion.Euler(90, 0, 90));
                tilePositions.Add(hex.transform.position);
                hex.transform.parent = this.transform;
                hex.name = $"Hex_{x}_{y}";
            }
        }
    }

    #endregion
}