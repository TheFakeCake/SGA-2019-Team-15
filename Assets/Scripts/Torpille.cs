using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpille : MonoBehaviour
{
    private AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        explosionSound = transform.Find("Explosion sound").gameObject.GetComponent<AudioSource>();
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
        bubbles.transform.localScale = transform.localScale;
        bubbleEmitter.Stop();

        explosionSound.transform.parent = null;
        explosionSound.Play();

        Destroy(bubbles, bubbleEmitter.main.duration);
        Destroy(explosionSound, explosionSound.clip.length);
        Destroy(this.gameObject);
    }
}
