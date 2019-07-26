using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRequin : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public float requinvelocity = 5;
    public AnimationCurve floattingCurve;
    private Vector2 startingPosition;
    public float floatingMultiplicator = 1;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        lastPosition = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float floating = floattingCurve.Evaluate(Time.time * requinvelocity);
        transform.position = startingPosition + new Vector2(floating * floatingMultiplicator, 0);


        if (transform.position.x < lastPosition.x)
        {//se tournera si va vers la droite et autre vers la gauche 
            transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().flipX = false;
        } // so immer in negativ wie klammer
        else
        {// mettre le if devant car si arrive a position normal alors revient sens de avant pas dans autre
            transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        lastPosition.x = transform.position.x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position - new Vector3(1, 0), 0.1f);
        Gizmos.DrawSphere(transform.position + new Vector3(1 + floatingMultiplicator, 0), 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Torpille")
        {
            this.gameObject.SetActive(false);
        }
    }
}