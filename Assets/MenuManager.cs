using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private int[] stats = { 0, 0, 0, 0 };

    [SerializeField] private GameObject[] records;
    [SerializeField] private TextMeshProUGUI[] display;

    [SerializeField] private GameObject exit;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            stats[i] = PlayerPrefs.GetInt(i.ToString(), 0);

            if (stats[i] < Stats.stats[i])
            {
                PlayerPrefs.SetInt(i.ToString(), Stats.stats[i]);
                records[i].SetActive(true);
            }

            if (i == 0)
            {
                TimeSpan t = TimeSpan.FromSeconds(stats[i]);
                display[i].text = t.ToString("mm':'ss");
            }

            else
                display[i].text = stats[i].ToString();
        }

        PlayerPrefs.Save();

#if UNITY_WEBGL
        exit.SetActive(false);
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
