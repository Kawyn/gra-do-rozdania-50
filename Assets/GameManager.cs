using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] public float maxTime = 150;
    [SerializeField] public float remainingTime = 150;

    public int dropModifier = 0;

    public GameObject imlazyenemy;



    public Vector2 minSpawnCoords;
    public Vector2 maxSpawnCoords;
    public float nextSpawntime = 5f;
    private float time = -5;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Stats.stats = new int[] { 0, 0, 0, 0 };
        remainingTime = maxTime;
        timeforspawn = 1;
        
    }

    private float timeforspawn;
    public bool gameOver = false;
    private void Update()
    {


        if (remainingTime <= 1)
        {
            remainingTime = 0;
            if (!gameOver)
                GameOver();
        return;

        }

        else
        {
            if (remainingTime > maxTime)
                remainingTime = maxTime;
            remainingTime -= Time.deltaTime;
            time += Time.deltaTime;
            timeforspawn -= Time.deltaTime;

        }

        if (timeforspawn < 0)
        {
            StartCoroutine(SpawnEnemy(imlazyenemy, new Vector2(
              Random.Range(minSpawnCoords.x, maxSpawnCoords.x),
              Random.Range(minSpawnCoords.y, maxSpawnCoords.y)
            )));
            if (nextSpawntime > 0.25f)
                nextSpawntime -= 0.25f;
            else nextSpawntime = 0.1f;
            timeforspawn = nextSpawntime;
        }
    }

    private void GameOver()
    {
        Stats.stats[0] = Mathf.FloorToInt(time);
        gameOver = true;
        Player.instance.Die();
        StartCoroutine(ToMenu());

    }

    IEnumerator ToMenu()
    {
        yield return new WaitForSeconds(2);

        InterfaceManager.instance.transition.SetTrigger("In");
        yield return new WaitForSeconds(1);
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
