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
    public float tempsInvulnerabilite = 1.5f;

    private Rigidbody2D rigidBody2D;
    private GameObject canon;
    private Torpille torpille;
    private Animator animatorInvulnerability;

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
        lastDashTime = -cooldownAcceleration;
        lastFireTime = -cooldownTir;
        lastHitTime = -tempsInvulnerabilite;
        ammoCount = munitionsInitiales;
    }

    // Update is called once per frame
    void Update()
    {
        // Applique la force de déplacement du sous-marin
        Vector2 force = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidBody2D.AddForce(force * forceMoteur);

        // Applique la force d'accélération (dash)
        if (Input.GetButtonDown("Dash") && canDash()) {
            rigidBody2D.AddForce(force.normalized * forceAcceleration, ForceMode2D.Impulse);
            lastDashTime = Time.time;
        }

        // Définit l'orientation horizontale du sous-marin
        if (force.x > 0) {
            setOrientation(true);
        } 
        else if (force.x < 0) {
            setOrientation(false);
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

    private void fire()
    {
        if (canFire()) {
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
