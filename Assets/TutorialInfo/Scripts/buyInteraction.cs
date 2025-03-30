using UnityEngine;

public class buyInteraction : MonoBehaviour
{
    public Canvas buyPromptCanvas;

    public BuyUI buyUI;

    private bool isPlayerNear;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buyPromptCanvas.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            buyPromptCanvas.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            buyPromptCanvas.enabled = false;
            buyUI.deSelectSlots(); // Add this line
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the sell UI
            buyUI.ToggleBuyUI();
            /*
            if (sellUI.isOpen)
            {
                sellUI.UpdateSellSlots();
            }
            */
        }
    }
}
