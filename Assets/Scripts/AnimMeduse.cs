﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMeduse : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public float medusevelocity=5;
    private Vector2 direction;
    public float amplideplace;

    public AnimationCurve floattingCurve;
    private Vector2 startingPosition;
    public float floatingMultiplicator= 1;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float floating = floattingCurve.Evaluate(Time.time * medusevelocity);//time=temps ecoulé donne vitesse pour des/mont
        transform.position = startingPosition + new Vector2(0, floating * floatingMultiplicator);// ici zone de flottage 
    }

    private void OnDrawGizmos()//permet de voir zone de flottage avec point noir 
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(transform.position - new Vector3(0, 1), 0.1f);
        Gizmos.DrawSphere(transform.position + new Vector3(0, 1 + floatingMultiplicator), 0.1f);
    }
}

