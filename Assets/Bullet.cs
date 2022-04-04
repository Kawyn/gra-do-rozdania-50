using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public Vector2 velocity;

    public int damage = 10;

    [SerializeField] private LayerMask mask;

    [Space]
    [SerializeField] private ParticleSystem shards;
    [SerializeField] private ParticleSystem dust;


    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime * 2, mask);

        transform.right = velocity;
        transform.position = transform.position + (Vector3)velocity * Time.deltaTime;

        if (hit)
        {
            GameObject target = hit.transform.gameObject;

            if (target.CompareTag("Enemy"))
            {
                target.GetComponent<Enemy>().ReciveDamage(damage);
                Stats.stats[2]++;
                Selfdestruction();
            }

            else if (target.CompareTag("Player"))
            {
                Player.instance.ReciveDamage(damage);
            
                Selfdestruction();
            }

            else if (target.CompareTag("Wall"))
            {
                if (shards)
                {
                    shards.transform.parent = null;
                    shards.Play();
                }

                Selfdestruction();
            }
        }
    }

    private void Selfdestruction()
    {
        if (dust)
        {
            dust.transform.parent = null;
            dust.Stop();
        }
     
        Destroy(gameObject);
    }
}
