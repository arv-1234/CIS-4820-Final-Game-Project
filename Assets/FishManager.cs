using UnityEngine;
using System.Collections.Generic;

public class FishManager {
    // Declare variables
    private List<Fish> commonFish;
    private List<Fish> rareFish;
    private List<Fish> legendaryFish;

    private int num;

    // Constructor: initialize all of the fish species to their rarity list
    public void initialize() {
        commonFish = new List<Fish>();
        rareFish = new List<Fish>();
        legendaryFish = new List<Fish>();

        commonFish.Add(new Fish("Shrimp", "Common"));
        commonFish.Add(new Fish("Squid", "Common"));
        commonFish.Add(new Fish("Flying Fish", "Common"));
        commonFish.Add(new Fish("Crab", "Common"));
        rareFish.Add(new Fish("Eel", "Rare"));
        rareFish.Add(new Fish("Salmon", "Rare"));
        rareFish.Add(new Fish("Tuna", "Rare"));
        legendaryFish.Add(new Fish("Sea Urchin", "Legendary"));
    }

    // Returns a random fish based on rarity
    public Fish GetRandomFish() {
        // Choose a random number from 1% to 100%
        num = Random.Range(1, 100);

        // Check which rarity was obtained: 1%-60% = Common, 61%-90% = Rare, 91%-100% = Legendary
        if (num >= 1 && num <= 60) {
            // Check which common fish was obtained
            num = Random.Range(0, commonFish.Count);
            return commonFish[num];
        } else if (num >= 61 && num <= 90) {
            // Check which rare fish was obtained
            num = Random.Range(0, rareFish.Count);
            return rareFish[num];
        } else {
            return legendaryFish[0];
        }
    }
}