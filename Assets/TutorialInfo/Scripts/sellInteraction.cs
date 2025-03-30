using UnityEngine;

public class sellInteraction : MonoBehaviour
{
    public Canvas sellPromptCanvas;

    public SellUI sellUI;

    private bool isPlayerNear;

    public slotItem[] sellSlots; // Assign these in Inspector
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sellPromptCanvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNear = true;
            sellPromptCanvas.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            sellPromptCanvas.enabled = false;
            sellUI.deSelectSlots(); // Add this line
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the sell UI
            sellUI.ToggleSellUI();

            if (sellUI.isOpen)
            {
                sellUI.UpdateSellSlots();
            }
        }
    }

   
}
