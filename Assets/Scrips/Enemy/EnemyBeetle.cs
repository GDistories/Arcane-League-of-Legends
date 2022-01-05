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

    new void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        movePos = rightPos;
        originalY = transform.position.y;
    }

    new void Update()
    {
        base.Update();
        EnemyMove(movePos.position);
    }

    private void EnemyMove(Vector2 position)
    {
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(movePos.position.x, transform.position.y), beetleSpeed * Time.deltaTime);
        // print(Vector2.Distance(transform.position, movePos.position));
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
}
