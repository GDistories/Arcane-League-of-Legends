using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5;
    [SerializeField] private float jumpSpeed = 6;
    [SerializeField] private float doubleJumpSpeed = 6;
    [SerializeField] private float restoreTime;
    private bool canDoubleJump = false;
    public Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private bool playerHasXAxisSpeed;
    private bool isGround;
    private bool isOneWayPlatform;
    
    public Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameAlive)
        {
            Flip();
            Run();
            Jump();
            OneWayPlatformCheck();
        }
        
    }

    private void Flip()
    {
        playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.0001f)
            {
                transform.localRotation = Quaternion.Euler(0,0,0);
            }
            
            if (myRigidbody.velocity.x < -0.0001f)
            {
                transform.localRotation = Quaternion.Euler(0,180,0);
            }
        }
    }

    private void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRun", playerHasXAxisSpeed);
    }

    private void Jump()
    {
        CheckGrounded();
        SwitchFallAnimation();
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnimator.SetBool("IsJump", true);
                Vector2 jumpVel = new Vector2(0, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    myAnimator.SetBool("IsJump", true);
                    myAnimator.SetBool("IsDoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0, doubleJumpSpeed);
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }
    
    private void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
                   || myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
                   || myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }
    
    private void SwitchFallAnimation()
    {
        if (myAnimator.GetBool("IsJump"))
        {
            if (myRigidbody.velocity.y < 0)
            {
                myAnimator.SetBool("IsJump", false);
                myAnimator.SetBool("IsFall", true);
            }
            else
            {
                myAnimator.SetBool("IsFall", false);
            }
        }else if (isGround)
        {
            myAnimator.SetBool("IsFall", false);
            myAnimator.SetBool("IsDoubleJump", false);
        }
    }

    private void OneWayPlatformCheck()
    {
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && moveY < -Mathf.Epsilon)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    private void RestorePlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}
