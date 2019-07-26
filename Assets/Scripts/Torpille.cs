using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpille : MonoBehaviour
{
    public AudioClip explosionSound;
    private AudioSource sourceExplosion;
    // Start is called before the first frame update
    void Start()
    {

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

    void Awake()
    {

        sourceExplosion = GetComponent<AudioSource>();
    }

    private void explode()
    {
        GameObject bubbles = transform.Find("Bulles").gameObject;
        ParticleSystem bubbleEmitter = bubbles.GetComponent<ParticleSystem>();

        bubbles.transform.parent = null;
        bubbleEmitter.Stop();
        sourceExplosion.PlayOneShot(explosionSound, 1F);

        Destroy(bubbles, bubbleEmitter.main.duration);
        Destroy(this.gameObject);
    }
}
