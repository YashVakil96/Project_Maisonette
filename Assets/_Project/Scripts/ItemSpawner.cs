using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : Singleton<ItemSpawner>
{
    [Header("Hex Grid Reference")]
    public HexGrid3D hexGrid; // Assign your HexGrid GameObject in the Inspector

    [Header("Item Prefabs (Assign in Order: Item1, Item2, Item3, Item4)")]
    public GameObject[] itemPrefabs; // Must have 4 elements

    [Header("Spawn Chances (Must Sum to 100)")]
    [Tooltip("Item1: 60%, Item2: 20%, Item3: 15%, Item4: 5%")]
    public int[] spawnChances = { 60, 20, 15, 5 };

    [Header("Spawn Height Offset")]
    public float yOffset = 0.5f; // Adjust to position items above hex tiles

    private List<Vector3> spawnPositions = new List<Vector3>();

    void Start()
    {
        if (itemPrefabs.Length != 4 || spawnChances.Length != 4)
        {
            Debug.LogError("Assign exactly 4 item prefabs and 4 spawn chances!");
            return;
        }

        if (!ValidateChances())
        {
            Debug.LogError("Spawn chances must sum to 100!");
            return;
        }

        // Get all hex tile positions from HexGrid
        
    }

    public void InitItemSpawner()
    {
        spawnPositions = hexGrid.tilePositions;
        Debug.Log(spawnPositions.Count);
        PopulateGrid();
    }

    bool ValidateChances()
    {
        Debug.Log("inside validate chances");
        int total = 0;
        foreach (int chance in spawnChances)
        {
            total += chance;
        }
        return total == 100;
    }

    void PopulateGrid()
    {
        Debug.Log("inside populate Grid");
        foreach (Vector3 pos in spawnPositions)
        {
            GameObject itemToSpawn = GetRandomItem();
            Vector3 spawnPos = new Vector3(pos.x, pos.y + yOffset, pos.z);
            Instantiate(itemToSpawn, spawnPos, Quaternion.identity, transform);
        }
    }

    GameObject GetRandomItem()
    {
        Debug.Log("inside random item");
        int randomValue = Random.Range(0, 100);
        int cumulative = 0;

        for (int i = 0; i < spawnChances.Length; i++)
        {
            cumulative += spawnChances[i];
            if (randomValue < cumulative)
            {
                return itemPrefabs[i];
            }
        }

        return itemPrefabs[0]; // Fallback to Item1
    }
}