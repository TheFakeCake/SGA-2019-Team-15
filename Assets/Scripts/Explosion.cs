using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector2 startingPosition;
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("Sous-marin");

        if (coll.gameObject.tag == "Sous-marin")
        {
            particleSystem.Play();
            
        }

        Destroy(gameObject);
    }

}
