using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAmountPowerUp : Ware
{
    public int modifier = 1;

    protected override void OnBuy()
    {
        Player.instance.pistol.maxBullets+= modifier;
        InterfaceManager.instance.AddEmptyBullet();
    }
}
