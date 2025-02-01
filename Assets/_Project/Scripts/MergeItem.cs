using UnityEngine;

public class MergeItem : MonoBehaviour
{
    public int itemLevel = 1; // Level of the item (e.g., 1, 2, 3)
    public bool isDraggable = true;

    private Vector3 offset;
    private bool isDragging = false;
    private HexGrid3D hexGrid; // Reference to the hex grid
    private Vector3 originalPosition; // Store the original position of the item

    void Start()
    {
        hexGrid = FindObjectOfType<HexGrid3D>(); // Find the hex grid in the scene
        originalPosition = transform.position; // Store the initial position
    }

    void OnMouseDown()
    {
        if (isDraggable)
        {
            offset = transform.position - GetMouseWorldPos();
            isDragging = true;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check if the item is being dropped on another item of the same level
        if (!TryMergeWithAdjacentItem())
        {
            // If no merge happens, snap back to the original position
            transform.position = originalPosition;
        }
    }

    bool TryMergeWithAdjacentItem()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider col in nearbyColliders)
        {
            MergeItem otherItem = col.GetComponent<MergeItem>();
            if (otherItem != null && otherItem != this && otherItem.itemLevel == this.itemLevel)
            {
                MergeItems(otherItem);
                return true; // Merge successful
            }
        }

        return false; // No merge happened
    }

    void MergeItems(MergeItem otherItem)
    {
        int newLevel = itemLevel + 1;
        // Vector3 newPosition = (transform.position + otherItem.transform.position) / 2;

        Destroy(this.gameObject);
        Destroy(otherItem.gameObject);

        // Spawn the new merged item
        GameObject newItemPrefab = GetPrefabForLevel(newLevel);
        if (newItemPrefab != null)
        {
            Instantiate(newItemPrefab, otherItem.transform.position, Quaternion.identity);
        }
    }

    GameObject GetPrefabForLevel(int level)
    {
        if (level < 4)
        {
            return ItemSpawner.Instance.itemPrefabs[level];
        }
        else
        {
            return null;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}