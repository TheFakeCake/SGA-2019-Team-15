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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Sous-marin") {
            explode();
        }
    }

    private void explode()
    {
        GameObject bubbles = transform.Find("Bulles").gameObject;
        ParticleSystem bubbleEmitter = bubbles.GetComponent<ParticleSystem>();

        bubbles.transform.parent = null;
        bubbleEmitter.Stop();
                
        Destroy(bubbles, bubbleEmitter.main.duration);
        Destroy(this.gameObject);
    }
}
