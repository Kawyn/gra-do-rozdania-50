using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity.normalized, velocity.magnitude);

        transform.Translate(velocity * Time.deltaTime);
        if (hit)
            Debug.Log(hit);
    }
}
