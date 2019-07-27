using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector2 startingPosition;
    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision coll)
    {
        /*
        Debug.Log("Sous-marin");

        if (coll.gameObject.tag == "Sous-marin" || coll.gameObject.tag == "Torpille")
        {
            particles.Play();
        }

        Destroy(gameObject);*/
    }

}
