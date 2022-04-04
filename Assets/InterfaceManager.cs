using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] remainingTimeText = new TextMeshProUGUI[2];
    [SerializeField] private Image remainingTimeBar;


    [SerializeField] private GameObject remainingBulletsPrefab;

    [SerializeField] private Transform remainingBulletsHolder;

    [SerializeField] private List<GameObject> remainingBullets;

    [SerializeField] private Vector2 remainingBulletsStart;
    [SerializeField] private Vector2 remainingBulletsOffset;

    public Animator transition;

    public static bool displayBuyIndicator = false;
    [SerializeField] private GameObject buyIndicator;

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

   
    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }
    private void Awake()
    {
        instance = this;
        camTransform = Camera.main.transform;
        Player.instance.onGunChange += SetupRemainingBulletsDisplay;
        Player.instance.onGunShot += UpdateRemainingBulletsDisplay;
        Player.instance.onGunShot += (object sender, OnGunShotEventArgs e) => ShakeCamera(0.07f, 0.07f);
    }


    private void Start()
    {

    }

    private void Update()
    {
        buyIndicator.SetActive(displayBuyIndicator);
        UpdateRemainingTimeDisplay();
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    private void UpdateRemainingTimeDisplay()
    {
        remainingTimeText[0].text = "<#F2A154>" + Mathf.Floor(GameManager.instance.remainingTime).ToString();
        remainingTimeText[1].text = "<#F2A154><size=50%>/" + GameManager.instance.maxTime.ToString() + "<#D3D3D3>SEC";

        remainingTimeBar.fillAmount = GameManager.instance.remainingTime / GameManager.instance.maxTime;
    }

    private void SetupRemainingBulletsDisplay(object sender, OnGunChangeEventArgs e)
    {
        foreach (GameObject o in remainingBullets)
            Destroy(o);

        remainingBullets = new List<GameObject>();

        for (int i = 0; i < e.gun.maxBullets; i++)
        {
            remainingBullets.Add(Instantiate(remainingBulletsPrefab, remainingBulletsHolder));
            remainingBullets[i].transform.localPosition = (Vector3)remainingBulletsOffset * i + (Vector3)remainingBulletsStart;
        }
        Debug.Log(remainingBullets.Count + " " +  e.gun.maxBullets);
        remainingBullets[e.gun.maxBullets - 1].transform.localPosition += new Vector3(0, 5, 0);

    }

    private void UpdateRemainingBulletsDisplay(object sender, OnGunShotEventArgs e)
    {

        if (e.gun.remainingBullets > 0)
            remainingBullets[e.gun.remainingBullets - 1].transform.localPosition += new Vector3(0, 5, 0);
    
        remainingBullets[e.gun.remainingBullets].transform.localPosition += new Vector3(0, -5, 0);

        Destroy(remainingBullets[e.gun.remainingBullets].transform.GetChild(0).gameObject);
    }

    public void AddEmptyBullet()
    {
        remainingBullets.Add(Instantiate(remainingBulletsPrefab, remainingBulletsHolder));
        remainingBullets[remainingBullets.Count - 1].transform.localPosition = (Vector3)remainingBulletsOffset * (remainingBullets.Count - 1) + (Vector3)remainingBulletsStart;

        Destroy(remainingBullets[remainingBullets.Count - 1].transform.GetChild(0).gameObject);

    }

    public void ShakeCamera(float a, float b)
    {
        if (shakeAmount < a || shakeDuration <= 0)
        {
            shakeAmount = a;
            shakeDuration = b;
        }
    }

    public static InterfaceManager instance;
}
