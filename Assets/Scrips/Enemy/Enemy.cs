using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    
    [SerializeField] protected int health;
    [SerializeField] private float flashTime = 0.2f;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private int damage = 1;

    private int currentHealth;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    
    
    // Start is called before the first frame update
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        currentHealth = health;
    }

    // Update is called once per frame
    public void Update()
    {
        isDie();
        Flip();
    }

    private void Flip()
    {
        // if ()
        // {
        //     transform.localRotation = Quaternion.Euler(0,0,0);
        // }
        //     
        // if ()
        // {
        //     transform.localRotation = Quaternion.Euler(0,180,0);
        // }
    }

    private void isDie()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        GameController.camShake.Shake();
    }
    
    private void FlashColor(float time)
    {
        spriteRenderer.color = Color.red;
        Invoke("ResetColor", time);
    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.DamagePlayer(damage);
        }
    }
}
