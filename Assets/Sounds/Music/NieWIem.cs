using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NieWIem : MonoBehaviour
{

    public AudioSource asource;
    public AudioSource bsource;
    public static NieWIem instance;
    void Start()
    {
        if (instance)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        asource.Play();
        bsource.PlayDelayed(asource.clip.length);
    }
}
