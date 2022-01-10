using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private int Blinks;
    [SerializeField] private float time;
    [SerializeField] private float timeBetweenDamage = 1f;

    private float nextDamageTime = 0;
    private Renderer myRenderer;
    private bool canTakeDamage = true;
    private bool Key1 = false;

    private ScreenFlash screenFlash;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        screenFlash = GetComponent<ScreenFlash>();
    }

    // Update is called once per frame
    void Update()
    {
        updateDamageTime();
        GodH();
    }
    
    public void DamagePlayer(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            HealthBar.HealthCurrent = health;
            BlinkPlayer(Blinks, time);
            screenFlash.FlashScreen();
            canTakeDamage = false;
            nextDamageTime = timeBetweenDamage + Time.time;
        }
        if (health <= 0)
        {
            GameController.isGameAlive = false;
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

    private void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            myRenderer.enabled = !myRenderer.enabled;
            yield return new WaitForSeconds(seconds);
        }

        myRenderer.enabled = true;
    }
    
    private void GodH()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Key1 = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Key1 = false;
        }
        if (Input.GetKeyDown(KeyCode.H) && Key1)
        {
            print("GodH");
            health += 2;
        }
    }
}
