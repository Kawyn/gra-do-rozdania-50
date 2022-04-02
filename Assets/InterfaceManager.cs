using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    private TextMeshProUGUI remainingTimeText;
    private Image remainingTimeBar;

    private List<Image> bullets;

    public GameObject bulletPrefab;


    public Vector2 magazineOffset;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    private void SetupBullets()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        return;
        remainingTimeText.text = (GameManager.instance.remainingTime % 1).ToString() + "<#F2A154><size=40%>SEC";    
        remainingTimeBar.fillAmount = GameManager.instance.remainingTime / GameManager.instance.maxTime;
    }

    public static InterfaceManager instance;
}
