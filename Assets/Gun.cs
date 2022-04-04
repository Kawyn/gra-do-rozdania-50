using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Gun 
{
  

    public abstract bool Shot(Vector2 position, Vector2 target);
}
