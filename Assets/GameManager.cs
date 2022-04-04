using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


[System.Serializable]
public struct Wave
{
    public float time;

    [System.Serializable]
    public struct SomeSuperbName
    {
        public int amount;
        public GameObject enemy;
    }

    public List<SomeSuperbName> enemies;
}

public class GameManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();


    public AudioClip shotClip;
    public AudioClip reciveDmgClip;
    public AudioClip mouseOverUpgradeCLip;
    public AudioClip buyClip;
    public AudioClip enemyShot;


    public AudioClip dieClip;

    [SerializeField] private GameObject indicator;
    [SerializeField] public float maxTime = 150;
    [SerializeField] public float remainingTime = 150;

    public int dropModifier = 0;

    public GameObject imlazyenemy;

    public int wave = 2;
    private float nextWaveTime = 0;


    private int amountOfWaves = 0;

    public Vector2 minSpawnCoords;
    public Vector2 maxSpawnCoords;
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
        Player.instance.onGunShot += (object sender, OnGunShotEventArgs e) => audioSource.PlayOneShot(shotClip);
        
    }
    public    AudioSource audioSource;
    private float timeforspawn;
    public bool gameOver = false;
    public bool spawnedOne = false;
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
            nextWaveTime += Time.deltaTime * (10 + amountOfWaves) / 10;
        }

        if (Mathf.Floor(Time.realtimeSinceStartup % 3) == 0)
        {
            if (!spawnedOne)
            {
                StartCoroutine(SpawnEnemy(imlazyenemy, new Vector2(
                Random.Range(minSpawnCoords.x, maxSpawnCoords.x),
                Random.Range(minSpawnCoords.y, maxSpawnCoords.y)
              )));
                spawnedOne = true;
            }
        }
        else
        {
            spawnedOne = false;
        }

        if (waves.Count > wave)
        {
            if (nextWaveTime > waves[wave].time)
            {
                foreach (Wave.SomeSuperbName e in waves[wave].enemies)
                {
                    for (int i = 0; i < e.amount; i++)
                    {

                        StartCoroutine(SpawnEnemy(e.enemy, new Vector2(
                          Random.Range(minSpawnCoords.x, maxSpawnCoords.x),
                          Random.Range(minSpawnCoords.y, maxSpawnCoords.y)
                        )));
                    }
                }

                nextWaveTime = 0;
                wave = Random.Range(0, waves.Count - 1);
                amountOfWaves++;
            }
        }
        else if(time > 300)
        {
            StartCoroutine(SpawnEnemy(imlazyenemy, new Vector2(
              Random.Range(minSpawnCoords.x, maxSpawnCoords.x),
              Random.Range(minSpawnCoords.y, maxSpawnCoords.y)
            )));
            nextWaveTime = 0;
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
        yield return new WaitForSeconds(Random.Range(0, 5));
        GameObject o = Instantiate(indicator, position, Quaternion.identity);

        yield return new WaitForSeconds(1);

        Destroy(o);
        Instantiate(enemy, position, Quaternion.identity);
    }


    public static GameManager instance;
}
