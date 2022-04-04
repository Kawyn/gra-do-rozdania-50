using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int timeDrop = 10;

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
        if (health > 0)
        {
            Move();

            if (!GameManager.instance.gameOver)
                Attack();
        }
    }

    protected abstract void Move();
    protected abstract void Attack();

    public void ReciveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }   

    protected virtual void Die() {

        GameManager.instance.remainingTime += timeDrop + GameManager.instance.dropModifier;
        Stats.stats[1]++;

        Destroy(gameObject);
    }
}
