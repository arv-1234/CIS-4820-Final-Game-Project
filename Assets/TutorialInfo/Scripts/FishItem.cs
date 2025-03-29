using UnityEngine;

public class FishItem
{
    private Fish fishes;
    private int quantity;
    private Sprite fishSprite;

    [TextArea]
    private string itemDesc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public FishItem(Fish fish, Sprite sprite)
    {
        fishes = fish;
        fishSprite = sprite;
        quantity = 1;
        itemDesc = getDescription();
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

    public string getDescription()
    {
        switch (fishes.getName())
        {
            case "Bass":
                return "A fierce freshwater predator with spiny fins. Known for its fighting spirit when hooked by anglers.";

            case "Beta Fish":
                return "Vibrant, territorial fish with flowing fins. Can breathe air using a special organ when water is stagnant.";

            case "Dolphin":
                return "Intelligent marine mammals that hunt in pods. Use echolocation and can leap high above waves.";

            case "Flying Fish":
                return "Glides over 50m using wing-like fins to escape predators. Often attracted to boat lights at night.";

            case "Moorish":
                return "Reef fish with striking black-and-yellow stripes. Their long dorsal fin sways gracefully in currents.";

            case "Shark":
                return "Ancient predators with electroreceptive senses. Sandpaper-like skin reduces drag while swimming.";

            case "Squid":
                return "Color-changing cephalopods that shoot ink clouds. Their intelligence rivals some mammals.";

            case "Turtle":
                return "Ancient mariners that migrate thousands of miles. Return to their birth beaches to lay eggs.";

            default:
                return "A mysterious creature with unusual adaptations.";
        }
    }
}
