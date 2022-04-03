using UnityEngine;
using TMPro;

public  abstract class Ware : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popup;
    [SerializeField] private float price;

    protected abstract void OnBuy();

    private void OnMouseEnter()
    {
        popup.text = gameObject.name + " - " + price + " sec";
        popup.gameObject.SetActive(true);
        
        InterfaceManager.displayBuyIndicator = true;
    }

    private void OnMouseExit()
    {
        popup.gameObject.SetActive(false);
    
        InterfaceManager.displayBuyIndicator = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.instance.remainingTime > price)
            {
                GameManager.instance.remainingTime -= price;
                OnBuy();
            }
        }
    }
}