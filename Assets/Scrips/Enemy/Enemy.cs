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
    [SerializeField] private GameObject floatPoint;
    [SerializeField] private GameObject dropCoin;
    [SerializeField] private float timeBetweenDamage = 1f;

    private float nextDamageTime = 0;
    private int currentHealth;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool canTakeDamage = true;
    
    
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
        updateDamageTime();
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
            Instantiate(dropCoin, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;
        GameObject floatP = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject;
        floatP.transform.GetChild(0).GetComponent<TextMesh>().text = "-" + damage;
        currentHealth -= damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        GameController.camShake.Shake();
        canTakeDamage = false;
        nextDamageTime = timeBetweenDamage + Time.time;
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
    
    private void updateDamageTime()
    {
        if (Time.time > nextDamageTime)
        {
            canTakeDamage = true;
        }
    }
}
