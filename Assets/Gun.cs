using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Gun : ScriptableObject
{
    public Sprite sprite;

    public abstract void Shot(Vector2 target);
}
