using UnityEngine;

public class FishItem
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

    public int  getQuantity()
    {
        return quantity;
    }

    public void AddQuantity()
    {
        quantity += 1;
    }

    public Sprite getSprite()
    {
        return fishSprite;
    }

    public string getFishName()
    {
        return fishes.getName();
    }
}
