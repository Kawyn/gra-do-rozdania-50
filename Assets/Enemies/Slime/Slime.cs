using UnityEngine;
using System.Collections;

public class Slime : Enemy
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float timeBetweenShots = 2f;


    [SerializeField] private float velocityModifier = 5;
    
    [SerializeField] private AnimationCurve xVelocityOverTime;
    [SerializeField] private AnimationCurve yVelocityOverTime;

    private float time = -2;
    private bool attacking = false;

    [SerializeField] private Animator animator;

    protected override void Move()
    {
           
        sr.flipX = Player.instance.transform.position.x < transform.position.x; 

        time += Time.deltaTime;

        if (attacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }

            Vector2 dir = Player.instance.transform.position - transform.position;
        float magnitude = dir.magnitude;

        dir.Normalize();

        if (magnitude > 10)
            rb.velocity += dir * Time.deltaTime;
        else if (magnitude < 5)
            rb.velocity += -dir * Time.deltaTime;
            
        rb.velocity += new Vector2(Mathf.Sin(time), Mathf.Cos(time)) * Time.deltaTime ;


        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 10);
    }

    protected override void Attack()
    {
        if (attacking)
            return;

        if (Mathf.Floor(time % (timeBetweenShots + 1)) == 0)
        {
            
            time += 1;
            StartCoroutine(AnimationSync());
        }
    }

    IEnumerator AnimationSync()
    {
        attacking = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.enemyShot);
        Vector2 direction = Player.instance.transform.position - transform.position;
        direction.Normalize();

        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<Bullet>().velocity = direction * 25;
        attacking = false;

    }

    protected override void Die() {

        GetComponent<Collider2D>().enabled = false;
        GameManager.instance.remainingTime += timeDrop + GameManager.instance.dropModifier;
        Stats.stats[1]++;
        animator.SetTrigger("Die");

        StartCoroutine(DelayDestory());
    }

    IEnumerator DelayDestory()
    {

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
}
