using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpille : MonoBehaviour
{
    public float force = 1;

    private Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        rigidBody2D.AddForce(transform.right * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
