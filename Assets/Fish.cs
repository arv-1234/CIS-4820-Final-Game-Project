using UnityEngine;
using System.Collections;

[System.Serializable]

// This class holds all of the information needed for each Fish
public class Fish {
    // Initialize variables (consider adding a fish model as another variable)
	public string fishName;
    public string rarity;
    public float price;

    // Constructor that allows the user to modify the variable values
    public Fish(string fishName, string rarity) {
        // Assign parameter values to the variable values
        this.fishName = fishName;
        this.rarity = rarity;

        // Update the price of the fish depending on its rarity
        if (rarity == "Common") {
            this.price = 15.0F;
        } else if (rarity == "Rare") {
            this.price = 30.0F;
        } else {
            this.price = 60.0F;
        }
    }
}