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

    private Rigidbody2D rigidBody2D;
    private GameObject canon;
    private Torpille torpille;

    private float lastDashTime;
    private float lastFireTime;
    private bool currentOrientation = true;
    private int ammoCount;
    private int hp = 3;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        canon = transform.Find("Canon").gameObject;
        torpille = projectile.GetComponent<Torpille>();
        lastDashTime = -cooldownAcceleration;
        lastFireTime = -cooldownTir;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ennemis")) {
            hp--;
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
}
