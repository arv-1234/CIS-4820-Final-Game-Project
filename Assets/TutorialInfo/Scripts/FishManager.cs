using UnityEngine;
using System.Collections.Generic;

// This class manages all of the fish
public class FishManager {
    // Declare variables
    private List<Fish> commonFish;
    private List<Fish> rareFish;
    private List<Fish> legendaryFish;

    private int num;

    // Initialize all of the fish species to their rarity list
    public void initialize() {
        commonFish = new List<Fish>();
        rareFish = new List<Fish>();
        legendaryFish = new List<Fish>();

        commonFish.Add(new Fish("Beta Fish", 1));
        commonFish.Add(new Fish("Salmon", 1));
        commonFish.Add(new Fish("Flying Fish", 1));
        commonFish.Add(new Fish("Tuna", 1));
        rareFish.Add(new Fish("Squid", 2));
        rareFish.Add(new Fish("Flounder", 2));
        rareFish.Add(new Fish("Turtle", 2));
        legendaryFish.Add(new Fish("Shark", 3));
    }

    // Returns a random fish based on chance
    public Fish getRandomFish() {
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