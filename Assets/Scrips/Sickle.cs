using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float tuning;
    
    private Rigidbody2D rb2d;
    private Transform playerTransform;
    private Transform sickleTransform;
    private Vector2 startSpeed;

    private CameraShake cameraShake;
    private bool isReturn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * speed;
        startSpeed = rb2d.velocity;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransform = GetComponent<Transform>();
        cameraShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotateSpeed);
        rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime;
        if(Mathf.Abs(rb2d.velocity.x) < 0.1f)
        {
            isReturn = true;
        }
        if (isReturn)
        {
            float posy = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);
            transform.position = new Vector3(transform.position.x, posy, 0.0f);
        }
        if(Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
