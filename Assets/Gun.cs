using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Gun : ScriptableObject
{
    public int maxBullets;
    public int remainingBullets;


    public Sprite sprite;

    public abstract void Shot(Vector2 position, Vector2 target);
}
