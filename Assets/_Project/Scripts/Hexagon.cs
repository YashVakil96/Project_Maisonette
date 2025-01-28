using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public int value = 1;
    public SpriteRenderer spriteRenderer;

    public void SetValue(int newValue)
    {
        value = newValue;
        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        // Change color or sprite based on value
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, value / 10f);
    }
}
