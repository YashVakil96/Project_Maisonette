using UnityEngine;

public class MergeItem : MonoBehaviour
{
    public int itemLevel = 1; // Level of the item (e.g., 1, 2, 3)
    public bool isDraggable = true;

    private Vector3 offset;
    private bool isDragging = false;

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
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        CheckForMerge();
    }

    void CheckForMerge()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider col in nearbyColliders)
        {
            MergeItem otherItem = col.GetComponent<MergeItem>();
            if (otherItem != null && otherItem != this && otherItem.itemLevel == this.itemLevel)
            {
                MergeItems(otherItem);
                break;
            }
        }
    }

    void MergeItems(MergeItem otherItem)
    {
        int newLevel = itemLevel + 1;
        Vector3 newPosition = (transform.position + otherItem.transform.position) / 2;

        Destroy(this.gameObject);
        Destroy(otherItem.gameObject);

        // Spawn the new merged item
        GameObject newItemPrefab = GetPrefabForLevel(newLevel);
        if (newItemPrefab != null)
        {
            Instantiate(newItemPrefab, newPosition, Quaternion.identity);
        }
    }

    GameObject GetPrefabForLevel(int level)
    {
        // Replace this with your logic to get the correct prefab for the level
        return ItemSpawner.Instance.itemPrefabs[level];
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}