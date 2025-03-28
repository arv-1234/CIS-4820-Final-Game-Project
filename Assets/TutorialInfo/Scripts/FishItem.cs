using UnityEngine;

public class FishItem : MonoBehaviour
{
    private Fish fishes;
    private int quantity;
    private Sprite fishSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public FishItem(Fish fish, Sprite sprite)
    {
        fishes = fish;
        fishSprite = sprite;
        quantity = 1;
    }

    public int  getQuant()
    {
        return quantity;
    }

    public void AddQuantity()
    {
        quantity += 1;
    }
}
