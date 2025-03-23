using UnityEngine;

// This class holds all of the information needed for each Fish
public class Fish {
    // Initialize variables (consider adding a fish model as another variable)
	private string fishName;
    private string rarity;
    private float price;

    // Constructor that allows for variable reassignment
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

    // Get functions that allows for variable viewing
    public string getName() {
        return fishName;
    }
    
    public string getRarity() {
        return rarity;
    }
    
    public float getPrice() {
        return price;
    }
}