using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public  abstract class Ware : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popup;
    [SerializeField] private float price;



    private void OnMouseEnter()
    {
        popup.text = gameObject.name + " - " + price + " sec";
        popup.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        popup.gameObject.SetActive(false);

    }

    private void OnMouseOver()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Kupiono");

        }
    }
}
