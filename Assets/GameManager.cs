using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float maxTime;
    [SerializeField] public float remainingTime;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        remainingTime -= Time.deltaTime;
    }
    

    public static GameManager instance;
}
