using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Gun : ScriptableObject
{
    public int maxBullets;
    public int remainingBullets;
    public float reloadTime = 1f;


    public Sprite sprite;

    public abstract bool Shot(Vector2 position, Vector2 target);
}
