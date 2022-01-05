using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;

    [SerializeField] private float timeBetweenDamage = 1f;

    private float nextDamageTime = 0;

    private bool canTakeDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateDamageTime();
    }
    
    public void DamagePlayer(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            canTakeDamage = false;
            nextDamageTime = timeBetweenDamage + Time.time;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void updateDamageTime()
    {
        if (Time.time > nextDamageTime)
        {
            canTakeDamage = true;
        }
    }
}
