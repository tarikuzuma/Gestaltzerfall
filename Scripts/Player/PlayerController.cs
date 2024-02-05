using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    private bool isFacingRight = true;

    private bool isGrounded;
    private bool isFalling;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private bool canDoubleJump;

    private Animator anim;

    private SpriteRenderer theSR;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    // Dashing components
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find things automatically without having to assign them yourself.
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (knockBackCounter <= 0)
        {

            // move on the x-axis but stay on the y-axis.
            // Use Unity's input system and get the value of the x-axis. 
            theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

            // Create a circle at that position with a radius of .2 and check and see what is in that ground layer, and if
            // there are anything colliding you, set it to true.
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            // If on the ground, Can Double Jump is True
            if (isGrounded)
            {
                canDoubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }

            if (Input.GetButtonUp("Jump") && theRB.velocity.y > 0)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * 0.5f);
            }

            if (Input.GetButtonDown("Jump"))
            {
                // Check if the player is Grounded and if not 
                if (isGrounded)
                {
                    // GetButton vs GetButtonDown.
                    // GetButton makes it so that the player, when pressing up, will perpetually jump.
                    // GetButtonDown makes it so that user only when clicked once.
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce * 1.1f);
                }
                else
                {
                    // Once Double Jump happens, of course, isGrounded will be false.
                    // If this is outside the else statement, it won't work since they will always be not grounded once
                    // player jumps.
                    if (canDoubleJump)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        canDoubleJump = false;

                    }
                }

            }

            // Lets player rotate
            if (theRB.velocity.x < 0)
            {
                isFacingRight = false;
                theSR.flipX = true;
            }
            else if (theRB.velocity.x > 0)
            {
                isFacingRight = true;
                theSR.flipX = false;
            }

            isFalling = theRB.velocity.y < 0;


        }
        else
        {
            knockBackCounter -= Time.deltaTime;

            if (!theSR.flipX)
            {
                theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
            }
            else
            {
                theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
            }
        }

        // Import to spell this the same way you spelled it in unity.
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFalling", isFalling);

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);

        anim.SetTrigger("hurt");
    }

    public void KnockBackVertical()
    {
        // Replensih DOuble Jump in each knockback vertical
        canDoubleJump = true;
        theRB.velocity = new Vector2(0f, knockBackForce * 3);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = theRB.gravityScale;
        theRB.gravityScale = 0f;

        float dashDirection = isFacingRight ? 1f : -1f;
        theRB.velocity = new Vector2(dashDirection * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);
        theRB.gravityScale = originalGravity;

        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    public bool GetIsFacingRight()
    {
        return isFacingRight;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}
