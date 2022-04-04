using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreTimePowerUp : Ware
{
    public int modifier = 1;
    protected override void OnBuy()
    {
        GameManager.instance.dropModifier += modifier ;
    }
}
