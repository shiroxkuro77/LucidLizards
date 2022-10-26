using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFalling : MonoBehaviour
{
    public Rigidbody2D enemyRigidBody2D;
    public float UnitsToMove;
    public float EnemySpeed;
    public bool _isFacingRight;
    private float _startPos;
    private float _endPos;

    public bool _moveRight = true;
    private Animator anim;

    // Use this for initialization
    public void Awake()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        _startPos = transform.position.x;
        _endPos = _startPos + UnitsToMove;
        _isFacingRight = transform.localScale.x > 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {

        if (_moveRight)
        {
            enemyRigidBody2D.AddForce(Vector2.right * EnemySpeed * Time.deltaTime);
            if (!_isFacingRight)
                Flip();
        }

        if (enemyRigidBody2D.position.x >= _endPos)
            _moveRight = false;

        if (!_moveRight)
        {
            enemyRigidBody2D.AddForce(-Vector2.right * EnemySpeed * Time.deltaTime);
            if (_isFacingRight)
                Flip();
        }
        if (enemyRigidBody2D.position.x <= _startPos)
            _moveRight = true;

        
        anim.SetBool("isMoving", Math.Abs(enemyRigidBody2D.velocity.x) > 0);
        

    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.y - transform.position.y > 0.5) {
                //enemyRigidBody2D.velocity = new Vector2(enemyRigidBody2D.velocity.x, 10);
            }
        }

    }
}