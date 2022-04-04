using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : Ware
{
    public int modifier = 1;

    protected override void OnBuy()
    {
        Player.instance.pistol.damage += modifier;
    }
}
