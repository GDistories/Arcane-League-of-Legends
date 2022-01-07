using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeetle : Enemy
{
    [SerializeField] private float beetleSpeed = 5;
    [SerializeField] private float startWaitTime;
    [SerializeField] private Transform movePos;
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform rightPos;
    
    
    private bool moveRight = true;
    private float originalY;
    private float waitTime;
    private PlayerController playerController;

    new void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos = rightPos;
        originalY = transform.position.y;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    new void Update()
    {
        base.Update();
        EnemyMove(movePos.position);
    }

    private void EnemyMove(Vector2 position)
    {
        Flip();
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(movePos.position.x, transform.position.y), beetleSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - movePos.position.x) < Mathf.Epsilon)
        {
            if (waitTime <= 0)
            {
                if (moveRight)
                {
                    movePos = leftPos;
                    moveRight = false;
                }
                else
                {
                    movePos = rightPos;
                    moveRight = true;
                }
                
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void Flip()
    {
        if (transform.position.x - movePos.position.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
        else if (transform.position.x - movePos.position.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0,180,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            playerController.myAnimator.SetBool("IsJump", true);
            playerController.myAnimator.SetBool("IsDoubleJump", true);
            Vector2 doubleJumpVel = new Vector2(0, 10);
            playerController.myRigidbody.velocity = Vector2.up * doubleJumpVel;
            TakeDamage(health/2);
        }
    }
}
