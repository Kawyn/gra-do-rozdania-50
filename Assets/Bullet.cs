using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;

    [HideInInspector] public Vector2 velocity;

    [SerializeField] private LayerMask mask;

    [SerializeField] private GameObject particles;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity.normalized, velocity.magnitude * Time.deltaTime, mask);
      
        if (hit)
        {
            GameObject target = hit.transform.gameObject;

            if (target.CompareTag("Enemy"))
                throw new System.NotImplementedException();
            else if (target.CompareTag("Wall"))
            {
                particles.transform.parent = null;
                particles.SetActive(true);
                Destroy(gameObject);
            
            }
        }
        else
        {
            transform.right = velocity;
            transform.position = transform.position + (Vector3)velocity * Time.deltaTime;
        }
    }
}
