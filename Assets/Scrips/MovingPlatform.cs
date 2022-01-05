using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float waitTime;

    [SerializeField] private Transform[] movePos;

    private int i;

    private Transform playerDefTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) < Mathf.Epsilon)
        {
            if (waitTime < 0)
            {
                i = i == 0 ? 1 : 0;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.parent = playerDefTransform;
        }
    }
}
