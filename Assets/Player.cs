using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    public static Player instance = null;
    
    public float m_Speed = 5;

    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer m_SpriteRenderer;


    [SerializeField] private Transform m_GunTransform;
    [SerializeField] private SpriteRenderer m_GunSpriteRenderer;

    [SerializeField] private Gun _Gun;

    public Gun m_Gun
    {
        get { return _Gun; }
        set
        {
            m_GunSpriteRenderer.sprite = value.sprite;
            _Gun = value;
        }
    }


    public Animator m_Animator;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();

        instance = this;
    }

    private void Update()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool flip = mousePosition.x > transform.position.x;
        
        m_SpriteRenderer.flipX = flip;
        m_GunSpriteRenderer.flipY = flip;

        m_GunTransform.right = -(mousePosition - (Vector2)transform.position);
        m_GunTransform.position = (mousePosition - (Vector2)transform.position).normalized + (Vector2)transform.position;
      
        if (Input.GetMouseButton(0))
            m_Gun.Shot(mousePosition);
    
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction = Vector2.ClampMagnitude(direction, 1);

        m_Animator.SetBool("isMoving", direction.magnitude > 0.1f);

        m_Rigidbody2D.velocity = direction * m_Speed;
    }
}
