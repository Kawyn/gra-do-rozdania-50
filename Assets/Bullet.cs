using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    [HideInInspector] public Vector2 velocity;

    [SerializeField] private LayerMask mask;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime, mask);

        transform.Translate(velocity * Time.deltaTime);

        if (hit)
        {
            GameObject target = hit.transform.gameObject;

            if (target.CompareTag("Enemy"))
                throw new System.NotImplementedException();
            else if (target.CompareTag("Wall"))
                Destroy(gameObject);
        }
    }
}
