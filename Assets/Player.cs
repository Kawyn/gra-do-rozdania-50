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



    [SerializeField] private Transform m_GunTransform;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;

    [SerializeField] public Pistol pistol;

    public event EventHandler<OnGunChangeEventArgs> onGunChange;
    public event EventHandler<OnGunShotEventArgs> onGunShot;

    public Animator m_Animator;

    private void Awake()
    {
        instance = this;    
    }
    public void ReciveDamage(int damage)
    {
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.reciveDmgClip);
        InterfaceManager.instance.ShakeCamera(0.2f, 0.2f);
        GameManager.instance.remainingTime -= damage;
    }

    public ParticleSystem shellParticle;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();

        onGunChange += (object sender, OnGunChangeEventArgs args) => this.gunSpriteRenderer.sprite = args.gun.sprite;
        onGunChange += (object sender, OnGunChangeEventArgs args) => args.gun.remainingBullets = args.gun.maxBullets;

        onGunShot += (object sender, OnGunShotEventArgs e) => shellParticle.Play();

        onGunChange?.Invoke(this, new OnGunChangeEventArgs { gun = pistol });
    }
    public GameObject shadow;

    public LayerMask wallMask;
    private void Update()
    {
        if (GameManager.instance.gameOver)
        {

            return;
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        bool flip = mousePosition.x > transform.position.x;
        
        m_SpriteRenderer.flipX = flip;
        gunSpriteRenderer.flipY = flip;

        m_GunTransform.right = -(mousePosition - (Vector2)transform.position);
        m_GunTransform.position = (mousePosition - (Vector2)transform.position).normalized + (Vector2)transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, m_GunTransform.position, wallMask);
            if (hit)
                return;

            if (pistol.Shot(m_GunTransform.position, mousePosition))
            {
                onGunShot?.Invoke(this, new OnGunShotEventArgs { gun = pistol });

                if (pistol.remainingBullets == 0)
                {

                    StartCoroutine(Reload(pistol.reloadTime));
                }
            }
            
        }
    }
    public GameObject shell;
    private void LateUpdate()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[100];

        int numParticelsAlive = shellParticle.GetParticles(particles);

        for (int i = 0; i < numParticelsAlive; i++)
        {
            if(particles[i].remainingLifetime <= 0.1f && particles[i].remainingLifetime != 0)
            {
                Debug.Log(particles[i].remainingLifetime);
                 Instantiate(shell, particles[i].position, Quaternion.identity);
                particles[i].remainingLifetime = -1;
            }
        }

        shellParticle.SetParticles(particles);
    }


    public void Die()
    {
        InterfaceManager.instance.ShakeCamera(1, 0.3f);

        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.dieClip);
        Destroy(shadow);
        m_Animator.SetTrigger("Die");
        Destroy(m_GunTransform.gameObject);
        m_Rigidbody2D.velocity = Vector2.zero;
    }
    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        if(!GameManager.instance.gameOver)
        onGunChange?.Invoke(this, new OnGunChangeEventArgs { gun = pistol });

    }

    private void FixedUpdate()
    {
        if (GameManager.instance.gameOver)
            return;

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction = Vector2.ClampMagnitude(direction, 1);

        m_Animator.SetBool("isMoving", direction.magnitude > 0.1f);

        m_Rigidbody2D.velocity = direction * m_Speed;
    }
}

public class OnGunChangeEventArgs : EventArgs { public Pistol gun; }
public class OnGunShotEventArgs : EventArgs { public Pistol gun; }
