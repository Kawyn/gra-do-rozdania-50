using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private Image remainingTimeBar;

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
        remainingTimeText.text = "<#F2A154>" + Mathf.Floor(GameManager.instance.remainingTime).ToString() + "<size=50%>/150<#D3D3D3>SEC";    
        remainingTimeBar.fillAmount = GameManager.instance.remainingTime / GameManager.instance.maxTime;
    }

    public static InterfaceManager instance;
}
