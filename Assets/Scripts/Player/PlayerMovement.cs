using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    #region walk
    public Vector2 move;
    public float speed = 5f;
    public float facingLeft = 1f;
    #endregion

    #region jump
    public float jump = 5f;
    public bool jumping = false;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool isOnGround = false;
    public float height = 0.7f;
    public float radius = 0.1f;
    public LayerMask groundLayer;
    #endregion

    public bool frozen = false;
    public float frozenX, frozenY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DoMove();
        CheckGround();
        BetterJump();
        FreezePlayer();
    }

    #region walking
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void DoMove()
    {
        rb.velocity = new Vector2(move.x * speed, rb.velocity.y);
        anim.SetFloat("horizontalSpeed", Mathf.Abs(rb.velocity.x));
        if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    #endregion

    #region jumping
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - height, transform.position.z), radius);
    }

    void CheckGround()
    {
        isOnGround = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - height), radius, groundLayer);
        anim.SetBool("isOnGround", isOnGround);
    }

    void OnJump(InputValue value)
    {
        jumping = value.Get<float>() == 1;
        if(jumping && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.ResetTrigger("jumping");
            anim.SetTrigger("jumping");
        }
    }

    void BetterJump()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !jumping)
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
       anim.SetFloat("verticalSpeed", rb.velocity.y);
    }
    #endregion

    void FreezePlayer()
    {
        if(fallMultiplier == 0)
        {
            if(!frozen)
            {
                frozenX = transform.position.x;
                frozenY = transform.position.y;
            }
            frozen = true;
            transform.position = new Vector3(frozenX, frozenY, transform.position.z);
            anim.SetBool("freeze", true);
        }
        else
        {
            anim.SetBool("freeze", false);
            frozen = false;
        }
    }
}
