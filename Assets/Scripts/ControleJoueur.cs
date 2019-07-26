using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJoueur : MonoBehaviour
{
    public float forceMoteur = 30;
    public float forceAcceleration = 20;
    public float cooldownAcceleration = 1;
    public float cooldownTir = 1;
    public Vector3 positionCanon;
    public GameObject projectile;
    public float forceProjectile = 500;
    public int munitionsInitiales = 3;
    public AudioClip marinSound;
    private AudioSource sourceMarin;
    public float tempsInvulnerabilite = 1.5f;

    private Rigidbody2D rigidBody2D;
    private GameObject canon;
    private Torpille torpille;
    public AudioClip shootSound;
    private AudioSource sourceShoot;
    private Animator animatorInvulnerability;
    private Animator animatorMovement;
    private ParticleSystem bubbleEmitter;

    private float lastDashTime;
    private float lastFireTime;
    private float lastHitTime;
    private bool currentOrientation = true;
    private int ammoCount;
    private int hp = 3;
    private bool isInvulnerable = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        canon = transform.Find("Canon").gameObject;
        torpille = projectile.GetComponent<Torpille>();
        animatorInvulnerability = transform.Find("Sprite").gameObject.GetComponent<Animator>();
        animatorMovement = GetComponent<Animator>();
        bubbleEmitter = transform.Find("Bubble emitter").gameObject.GetComponent<ParticleSystem>();
        lastDashTime = -cooldownAcceleration;
        lastFireTime = -cooldownTir;
        lastHitTime = -tempsInvulnerabilite;
        ammoCount = munitionsInitiales;

        AudioSource[] srcs = GetComponents<AudioSource>();
        sourceMarin = srcs[0];
        sourceMarin.Play();
        sourceShoot = srcs[1];
    }

    // Update is called once per frame
    void Update()
    {
        // Applique la force de déplacement du sous-marin
        Vector2 force = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody2D.AddForce(force * forceMoteur);

        if (canDash()) {
            if (Input.GetButtonDown("Dash")) {
                // Dash
                animatorMovement.SetBool("dash", true);
                rigidBody2D.AddForce(force.normalized * forceAcceleration, ForceMode2D.Impulse);
                lastDashTime = Time.time;
            }
        } else {
            animatorMovement.SetBool("dash", false);
        }

        // Définit l'orientation horizontale du sous-marin
        if (force.x > 0) {
            setOrientation(true);
        } 
        else if (force.x < 0) {
            setOrientation(false);
        }

        // Animation de mouvement et bulles
        if (force.magnitude > 0) {
            animatorMovement.SetBool("moving", true);
            if (! bubbleEmitter.isEmitting) {
                bubbleEmitter.Play();
            }
        } else {
            animatorMovement.SetBool("moving", false);
            if (bubbleEmitter.isEmitting) {
                bubbleEmitter.Stop();
            }
        }

        // Tir une torpille
        if (Input.GetButtonDown("Fire1")) {
            fire();
        }

        // Termine l'invulnérabilité si nécessaire
        if (isInvulnerable && Time.time > lastHitTime + tempsInvulnerabilite)
        {
            isInvulnerable = false;
            animatorInvulnerability.SetBool("invulnerability", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ennemis") && ! isInvulnerable) {
            loseHp();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Decors solide") && ! isInvulnerable) {
            loseHp();
        }
    }

    private bool canDash()
    {
        return (Time.time >= lastDashTime + cooldownAcceleration);
    }

    private void setOrientation(bool orientation)
    {
        if (currentOrientation != orientation) {
            transform.Rotate(new Vector3(0, 180, 0));
            currentOrientation = orientation;
        }
    }

    private bool canFire()
    {
        return (ammoCount > 0 && Time.time >= lastFireTime + cooldownTir);
    }

    void Awake()
    {
        sourceShoot = GetComponent<AudioSource>();
    }

    private void fire()
    {
        if (canFire()) {
            sourceShoot.PlayOneShot(shootSound, 4F);
            GameObject newProjectile = Object.Instantiate(projectile, canon.transform.position, canon.transform.rotation);
            Vector3 projectileDirection = currentOrientation ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(projectileDirection * forceProjectile);
            ammoCount--;
            lastFireTime = Time.time;
        }
    }

    private void loseHp()
    {
        hp--;
        animatorInvulnerability.SetBool("invulnerability", true);
        isInvulnerable = true;
        lastHitTime = Time.time;
    }

    public int getHp()
    {
        return hp;
    }
}
