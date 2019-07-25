using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMine : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public float tetevelocity = 5;
    public AnimationCurve floattingCurve;
    private Vector2 startingPosition;
    public float floatingMultiplicator = 1;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float floating = floattingCurve.Evaluate(Time.time * tetevelocity);
        transform.position = startingPosition + new Vector2(0, floating * floatingMultiplicator);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position - new Vector3(0, 1), 0.1f);
        Gizmos.DrawSphere(transform.position + new Vector3(0, 1 + floatingMultiplicator), 0.1f);
    }
}
