using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5;
    [SerializeField] private float jumpSpeed = 6;
    [SerializeField] private float doubleJumpSpeed = 6;
    [SerializeField] private float climbSpeed = 5;
    [SerializeField] private float restoreTime;

    private float originalGravity;
    private bool canDoubleJump = true;
    public Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private PolygonCollider2D myCollider;
    private bool playerHasXAxisSpeed;
    private bool isGround;
    private bool isOneWayPlatform;
    private bool isLadder;
    private bool isClimbing;
    private bool isLight = true;
    private GameObject playerFlashlight;

    // private bool isJumping;
    // private bool isFalling;
    
    public Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        myCollider = GetComponent<PolygonCollider2D>();
        originalGravity = myRigidbody.gravityScale;
        playerFlashlight = GameObject.FindGameObjectWithTag("PlayerFlashlight");
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
            Climb();
            Light();
        }
        
    }
    
    private void Climb()
    {
        CheckLeadder();
        if (isLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myRigidbody.gravityScale = 0;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);
            }
            else
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
            }
        }
        else
        {
            myRigidbody.gravityScale = originalGravity;
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
            if(isLadder)
                return;
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

    private void CheckLeadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
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

        // if (!myCollider.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform")) 
        //     && !myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform")))
        // {
        //     gameObject.layer = LayerMask.NameToLayer("Player");
        // }
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

    private void Light()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("L");
            isLight = !isLight;
            if (isLight)
            {
                playerFlashlight.SetActive(true);
            }
            else
            {
                playerFlashlight.SetActive(false);
            }
        }
    }
}
