using UnityEngine;

public class buyInteraction : MonoBehaviour
{
    public Canvas buyPromptCanvas;

    public BuyUI buyUI;

    private bool isPlayerNear;
    
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
            buyUI.deSelectSlots(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the buy UI
            buyUI.ToggleBuyUI();
          
        }
    }
}
