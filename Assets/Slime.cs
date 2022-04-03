using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float timeBetweenShots = 2f;


    [SerializeField] private float velocityModifier = 5;
    
    [SerializeField] private AnimationCurve xVelocityOverTime;
    [SerializeField] private AnimationCurve yVelocityOverTime;

    private float time;

    protected override void Move()
    {
        sr.flipX = Player.instance.transform.position.x < transform.position.x; 

        time += Time.deltaTime;


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
        if (Mathf.Floor(time % (timeBetweenShots + 1)) == 0)
        {

            Vector2 direction = Player.instance.transform.position - transform.position;
            direction.Normalize();

            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            b.GetComponent<Bullet>().velocity = direction * 25;

            time += 1;
        }
    }
}
