using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] records;
    [SerializeField] private TextMeshProUGUI[] display;

    [SerializeField] private GameObject exit;

    public Animator transition;
    private void Start()
    {
        if (Stats.stats[0] == 0)
            transition.gameObject.SetActive(false);

        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt(i.ToString(), 0) < Stats.stats[i])
            {
                PlayerPrefs.SetInt(i.ToString(), Stats.stats[i]);
                records[i].SetActive(true);
            }

            if (i == 0)
            {
                TimeSpan t = TimeSpan.FromSeconds(Stats.stats[i]);
                display[i].text = t.ToString("mm':'ss");
            }

            else
                display[i].text = Stats.stats[i].ToString();
        }

        PlayerPrefs.Save();

#if UNITY_WEBGL
        exit.SetActive(false);
#endif
    }

    public void StartGame()
    {
        transition.gameObject.SetActive(true);
        transition.SetTrigger("In"); 
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(1);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
