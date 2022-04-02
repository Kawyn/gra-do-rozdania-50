using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public float m_Speed = 5;

    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer m_SpriteRenderer;


    [SerializeField] private Gun defaultGun;

    [SerializeField] private Transform m_GunTransform;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;

    [SerializeField] private Gun _Gun;

    public event EventHandler<OnGunChangeEventArgs> onGunChange;
    public event EventHandler<OnGunShotEventArgs> onGunShot;


    public Gun m_Gun
    {
        get { return _Gun; }
        set
        {
            onGunChange?.Invoke(this, new OnGunChangeEventArgs { gun = value });

            _Gun = value;
        }
    }

    public Animator m_Animator;

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();

        onGunChange += (object sender, OnGunChangeEventArgs args) => this.gunSpriteRenderer.sprite = args.gun.sprite;
        onGunChange += (object sender, OnGunChangeEventArgs args) => args.gun.remainingBullets = args.gun.maxBullets;

        m_Gun = defaultGun;
    }

    private void Update()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool flip = mousePosition.x > transform.position.x;
        
        m_SpriteRenderer.flipX = flip;
        gunSpriteRenderer.flipY = flip;

        m_GunTransform.right = -(mousePosition - (Vector2)transform.position);
        m_GunTransform.position = (mousePosition - (Vector2)transform.position).normalized + (Vector2)transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            if (m_Gun.Shot(m_GunTransform.position, mousePosition)) 
            onGunShot?.Invoke(this, new OnGunShotEventArgs { gun = this.m_Gun });
            
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction = Vector2.ClampMagnitude(direction, 1);

        m_Animator.SetBool("isMoving", direction.magnitude > 0.1f);

        m_Rigidbody2D.velocity = direction * m_Speed;
    }
}

public class OnGunChangeEventArgs : EventArgs { public Gun gun; }
public class OnGunShotEventArgs : EventArgs { public Gun gun; }
