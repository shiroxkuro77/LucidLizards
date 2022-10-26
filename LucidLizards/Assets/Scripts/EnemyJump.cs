using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public Rigidbody2D enemyRigidBody2D;
    public float EnemySpeed = 200;
    private float _startPos;

    public bool _isGrounded = true;
    private Animator anim;

    // Use this for initialization
    public void Awake()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        _startPos = transform.position.y;
        anim = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (_isGrounded)
        {
            enemyRigidBody2D.AddForce(Vector2.up * EnemySpeed);
        }

        if (enemyRigidBody2D.position.y <= _startPos && enemyRigidBody2D.velocity.y == 0)
        {
            _isGrounded = true;
            
        }
        else {
            _isGrounded = false;
        }

        anim.SetBool("isMoving", Math.Abs(enemyRigidBody2D.velocity.x) > 0);

    }


}
