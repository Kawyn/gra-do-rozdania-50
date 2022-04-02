using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Pistol", menuName = "Guns/Pistol")]
public class Pistol : Gun
{

    public float reloadTime = 1;
    public int damage = 5;
    public float speed = 25;

    public GameObject bullet;

    private void Awake()
    {
        remainingBullets = maxBullets;
    }

    public override bool Shot(Vector2 position, Vector2 target)
    {
        if (remainingBullets == 0)
            return false;

        Vector2 direction = target - position;
        direction.Normalize();

        GameObject b = Instantiate(bullet, position, Quaternion.identity);
        b.GetComponent<Bullet>().velocity = direction * speed;

        remainingBullets--;

        return true;
    }
}
