using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pistol:MonoBehaviour
{
    public int maxBullets;
    public int remainingBullets;
    public float reloadTime = 1f;


    public Sprite sprite;

    public int damage = 5;
    
    public float speed = 25;

    public GameObject bullet;

   
    public  bool Shot(Vector2 position, Vector2 target)
    {
        if (remainingBullets == 0)
            return false;

        Vector2 direction = target - position;
        direction.Normalize();

        GameObject b = Instantiate(bullet, position, Quaternion.identity);
        b.GetComponent<Bullet>().velocity = direction * speed;
        b.GetComponent<Bullet>().damage = damage;

        remainingBullets--;

        return true;
    }
}
