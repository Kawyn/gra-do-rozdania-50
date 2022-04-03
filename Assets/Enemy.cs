using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int health;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        Move();
        Attack();
    }

    protected abstract void Move();
    protected abstract void Attack();

    public void ReciveDamage(int damage)
    {
        health -= damage;
    }
}
