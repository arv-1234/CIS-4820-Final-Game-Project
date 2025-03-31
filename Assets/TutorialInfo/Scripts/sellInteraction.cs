using UnityEngine;

public class sellInteraction : MonoBehaviour
{
    public Canvas sellPromptCanvas;

    public SellUI sellUI;

    private bool isPlayerNear;

    public slotItem[] sellSlots; 
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
            sellUI.deSelectSlots();
        }
    }

    
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
