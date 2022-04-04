using UnityEngine;
using TMPro;

public  abstract class Ware : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI message;

    [SerializeField] private float price;

    protected abstract void OnBuy();

    private void OnMouseEnter()
    {
        message.text = gameObject.name + " - " + price + " sec";
        popup.gameObject.SetActive(true);
           
        GetComponent<Animator>().SetTrigger("Shine");

        InterfaceManager.displayBuyIndicator = true;
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.mouseOverUpgradeCLip);

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
                Stats.stats[3]++;
                GameManager.instance.remainingTime -= price;
                OnBuy();
                GameManager.instance.audioSource.PlayOneShot(GameManager.instance.buyClip);

            }
        }
    }
}