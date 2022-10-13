using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpDown : MonoBehaviour
{
    public Rigidbody2D enemyRigidBody2D;
    public int UnitsToMove = 2;
    public float EnemySpeed = 200;
    public bool _isFacingRight;
    private float _startPos;
    private float _endPos;

    public bool _moveUp = true;

    // Use this for initialization
    public void Awake()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        _startPos = transform.position.y;
        _endPos = _startPos + UnitsToMove;
    }

    public void FixedUpdate()
    {
        if (_moveUp)
        {
            enemyRigidBody2D.AddForce(Vector2.up * EnemySpeed);
        }

        if (enemyRigidBody2D.position.y >= _endPos)
            _moveUp = false;

        if (!_moveUp)
        {
            enemyRigidBody2D.AddForce(Vector2.down * EnemySpeed);
        }

        if (enemyRigidBody2D.position.y <= _startPos)
            _moveUp = true;

    }

}
