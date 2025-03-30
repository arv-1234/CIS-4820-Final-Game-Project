using UnityEngine;

// This class manages all of the fish
public class FishManager {
    // Declare variables
    private Fish[] common, rare, legendary;
    private int chance, index;

    // Initialize all of the fish species to their rarity array
    public void initialize() {
        common = new Fish[4];
        rare = new Fish[3];
        legendary = new Fish[1];

        common[0] = new Fish("Bass", 1);
        common[1] = new Fish("Beta Fish", 1);
        common[2] = new Fish("Flying Fish", 1);
        common[3] = new Fish("Moorish", 1);

        rare[0] = new Fish("Dolphin", 2);
        rare[1] = new Fish("Squid", 2);
        rare[2] = new Fish("Turtle", 2);

        legendary[0] = new Fish("Shark", 3);
    }

    // Returns a random fish based on chance
    public Fish getRandomFish() {
        // Choose a random number from 1%-100%
        chance = Random.Range(1, 101);

        // Check which rarity was obtained: 1%-60% = Common, 61%-90% = Rare, 91%-100% = Legendary
        if (chance >= 1 && chance <= 60) {
            // Check which common fish was obtained
            index = Random.Range(0, 4);
            return common[index];
        } else if (chance >= 61 && chance <= 90) {
            // Check which rare fish was obtained
            index = Random.Range(0, 3);
            return rare[index];
        } else {
            return legendary[0];
        }
    }
}