using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float time;
    private Animator myAnimator;

    private PolygonCollider2D myPolygonCollider;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        myPolygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            myPolygonCollider.enabled = true;
            myAnimator.SetTrigger("Attack");
            StartCoroutine(disableHitBox());
        }
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        myPolygonCollider.enabled = false;
    }
}
