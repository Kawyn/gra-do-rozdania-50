using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int health;


    protected abstract void Move();
    protected abstract void Attack();

    public void ReciveDamage(int damage)
    {
        health -= damage;
    }
}
