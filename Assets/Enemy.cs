using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int timeDrop = 10;

    [SerializeField] private int maxHealth;
    public int health;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        health = maxHealth;
    }

    protected void Update()
    {
        Move();

        if (GameManager.instance.isDead == false)
            Attack();
    }

    protected abstract void Move();
    protected abstract void Attack();

    public void ReciveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }   

    protected void Die() {

        GameManager.instance.remainingTime += timeDrop;

        Destroy(gameObject);
    }
}
