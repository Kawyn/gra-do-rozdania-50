using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Pistol", menuName = "Guns/Pistol")]
public class Pistol : Gun
{
    public override void Shot(Vector2 target)
    {
        Debug.DrawLine(Player.instance.transform.position, target);
    }
}
