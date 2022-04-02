using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int health;


    public void ReciveDamage(int damage)
    {
        health -= damage;
    }
}
