using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] public float maxTime = 150;
    [SerializeField] public float remainingTime = 150;


    public GameObject imlazyenemy;

    public Vector2 minSpawnCoords;
    public Vector2 maxSpawnCoords;

    private float time;
   

    private void Awake()
    {
        instance = this;
    }

   public bool isDead = false;

    private void Update()
    {
        if (remainingTime <= 0)
        {
            remainingTime = 0;

            if(!isDead)
            GameOver();
        }
        else
        {

            if (remainingTime > maxTime)
                remainingTime = maxTime;
            remainingTime -= Time.deltaTime;
            time += Time.deltaTime;


        }
        if (Mathf.Floor(time % 10) == 0)
        {
            StartCoroutine(SpawnEnemy(imlazyenemy, new Vector2(
              Random.Range(minSpawnCoords.x, maxSpawnCoords.x),
              Random.Range(minSpawnCoords.y, maxSpawnCoords.y)
            )));
            
            time++;
        }
    }

    private void GameOver()
    {
        Player.instance.m_Animator.SetTrigger("Die");
        Player.instance.Die();
        
        Stats.stats[0] = Mathf.FloorToInt(time);

        StartCoroutine(ToMenu());
        isDead = true;
    }
    
    IEnumerator ToMenu()
    {
        yield return new WaitForSeconds(3f);

        InterfaceManager.TransitionIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(0);

    }
    IEnumerator SpawnEnemy(GameObject enemy, Vector2 position)
    {
        GameObject o = Instantiate(indicator, position, Quaternion.identity);

        yield return new WaitForSeconds(1);

        Destroy(o);
        Instantiate(enemy, position, Quaternion.identity);
    }


    public static GameManager instance;
}
