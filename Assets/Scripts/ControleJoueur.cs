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
    public int munitionsInitiales = 3;

    private Rigidbody2D rigidBody2D;

    private float lastDashTime = 0;
    private float lastFireTime = 0;
    private bool currentOrientation = true;
    private int ammoCount;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
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
        return (Time.time >= lastFireTime + cooldownTir);
    }

    private void fire()
    {
        if (ammoCount > 0 && canFire()) {
            Object.Instantiate(projectile, transform.position + positionCanon, new Quaternion(0, 0, 0, 0));
            ammoCount--;
            lastFireTime = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        // Affiche une sphere à la position du canon
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + positionCanon, 0.07f);
    }
}
