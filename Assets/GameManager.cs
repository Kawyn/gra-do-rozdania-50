using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        remainingTime -= Time.deltaTime;
        time += Time.deltaTime;

        if (Mathf.Floor(time % 10) == 0)
        {
            StartCoroutine(SpawnEnemy(imlazyenemy, new Vector2(
              Random.Range(minSpawnCoords.x, maxSpawnCoords.x),
              Random.Range(minSpawnCoords.y, maxSpawnCoords.y)
      )      ));
            time++;
        }
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
