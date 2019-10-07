using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    public float maxSpeed = 10f;


    public Transform groundChecker;
    public float groundRadius = 0.2f;
    public float spaceForce = 1f;
    public LayerMask groundLayer;
    private bool isGrounded = true;


    private Animator animator;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, spaceForce));
        }
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundRadius, groundLayer);

        animator.SetBool("Ground", isGrounded);
        animator.SetFloat("VerticalSpeed", rigidbody2D.velocity.y);

        if (!isGrounded)
            return;

        float move = Input.GetAxis("Horizontal");

        if (move < 0)
        {
            move = 0;
        }

        animator.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
    }
}
