using UnityEngine;

// This class holds all of the information needed for each Fish
public class Fish {
    // Declare variables (consider adding a fish model as another variable)
	private string fishName;
    private int rarity;
    private float price;
    private bool isNew;

    // Constructor that allows for variable reassignment
    public Fish(string fishName, int rarity) {
        // Assign parameter values to the variable values
        this.fishName = fishName;
        this.rarity = rarity;

        // Update the price of the fish depending on its rarity
        // * = common, ** = rare, *** = legendary
        if (rarity == 1) {
            this.price = 15.0F;
        } else if (rarity == 2) {
            this.price = 30.0F;
        } else {
            this.price = 60.0F;
        }

        isNew = true;
    }

    // Get functions that allow variable viewing
    public string getName() {
        return fishName;
    }
    
    public int getRarity() {
        return rarity;
    }
    
    public float getPrice() {
        return price;
    }
    
    public bool getIsNew() {
        return isNew;
    }

    public void setIsNew() {
        isNew = false;
    }
}