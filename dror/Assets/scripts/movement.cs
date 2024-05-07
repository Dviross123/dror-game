using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("physics")]
    public float speed;
    public Rigidbody2D rb;
    public float HorizontalInput;
    public float jumpPower;

    [Header("ground check")]
    public Transform checkGroundTras;
    public LayerMask groundLM;

    [Header("animation")]
    public Animator animator;

    [Header("flip player")]
    public bool isFacingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        isFacingRight = true;
    }

    void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(speed * HorizontalInput, rb.velocity.y);

        changeAnim();

       StartCoroutine(checkJump());

        if (HorizontalInput == 1) 
        {
            if (!isFacingRight)
                flipPlayer();
        }
        else if (HorizontalInput == -1)
        {
            if (isFacingRight)
                flipPlayer();
        }
    }

    public bool checkGrounded() 
    {
        if (Physics2D.Raycast(checkGroundTras.position, Vector2.down, 0.2f, groundLM))
        {
           return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator checkJump()
    {
        if (Input.GetButtonDown("Jump") && checkGrounded()) 
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("jump", true);
            yield return new WaitForSeconds(1f);
            animator.SetBool("jump", false);
        }
    }

    public void changeAnim() 
    {
        if (HorizontalInput != 0)
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);
    }

    public void flipPlayer()
    {
        if(isFacingRight)
        transform.rotation = Quaternion.Euler(0f, 180f, 0f); 
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        isFacingRight = !isFacingRight; 

    }
}
