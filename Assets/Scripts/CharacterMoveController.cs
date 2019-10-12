using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    public AudioClip jumpSound, deathSound;

    public Vector3 startPoint = new Vector3(-10, -3, -5);
    public float delayAfterRespawing = 5f;

    public float maxSpeed = 10f;

    public float buttomBound = -6f;

    public float boxcastSize = 0.15f;
    public float minDistanseToObjects = 0.5f;

    public Transform groundChecker;
    public float groundRadius = 0.2f;
    public Vector2 spaceForce = new Vector2(25f, 350f);
    public LayerMask groundLayer;
    private bool isGrounded = true;

    public ParticleSystem bloodSystem;

    private bool finished = false;
    private bool movable = true;

    private Animator animator;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (!finished)
        {
            CheckJump();

            CheckFocusedInCamera();

            CheckGrounded();

            CheckMovenment();
        }
    }
    private void CheckJump()
    {
        if (isGrounded && movable && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            animator.SetFloat("VerticalSpeed", rigidbody2D.velocity.y);
            rigidbody2D.AddForce(spaceForce);

            GetComponent<AudioSource>().clip = jumpSound;
            GetComponent<AudioSource>().Play();
        }
    }
    private void CheckFocusedInCamera()
    {
        if (transform.position.y < buttomBound)
        {
            Respawn();
        }
    }



    void FixedUpdate()
    {
        if (!finished)
        {
            //CheckGrounded();

            //CheckMovenment();
        }
    }
    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundRadius, groundLayer);

        animator.SetBool("Ground", isGrounded);
        animator.SetFloat("VerticalSpeed", rigidbody2D.velocity.y);
    }
    private void CheckMovenment()
    {
        Vector2 pointForwardPlayer = new Vector2(transform.position.x + 0.5f, transform.position.y);
        RaycastHit2D hitPoint = Physics2D.BoxCast(pointForwardPlayer,
                                                  new Vector2(boxcastSize, boxcastSize),
                                                  0,
                                                  Vector2.right);

        if (hitPoint)
        {
            float distance = hitPoint.point.x - transform.position.x;

            if (distance <= minDistanseToObjects)
            {
                return;
            }
        }

        if (movable)
        {
            float move = Input.GetAxis("Horizontal");

            move = FreezeLeftMovenment(move);

            animator.SetFloat("Speed", Mathf.Abs(move));

            rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
        }
    }
    private float FreezeLeftMovenment(float move)
    {
        if (move < 0)
        {
            move = 0;
        }

        return move;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Coin"))
        {
            collision.gameObject.GetComponent<TakeCoin>().Play();
        }
        if (collision.tag.Equals("Enemy"))
        {
            bloodSystem.transform.position = transform.position;
            bloodSystem.Play();
            Respawn();
        }
        if (collision.tag.Equals("Chest"))
        {
            Animator animator = collision.gameObject.GetComponent<Animator>();
            animator.SetBool("Opened", true);
            collision.gameObject.GetComponent<ParticleSystem>().Play();

            collision.gameObject.GetComponent<Chest>().Play();
            Finish();
        }
    }
    private void FreezeMovability()
    {
        movable = false;
        animator.SetBool("Movable", false);
        animator.SetBool("Ground", true);
        animator.SetFloat("Speed", 0f);
        animator.SetFloat("VerticalSpeed", 0f);
    }
    private void Respawn()
    {
        FreezeMovability();
        rigidbody2D.velocity = new Vector3(0, 0, 0);
        transform.position = startPoint;
        Invoke("SetMovable", delayAfterRespawing);
        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>().Play();
    }
    public void Restart()
    {
        FreezeMovability();
        rigidbody2D.velocity = new Vector3(0, 0, 0);
        transform.position = startPoint;
        Invoke("SetMovable", delayAfterRespawing);
    }
    private void SetMovable()
    {
        movable = true;
        animator.SetBool("Movable", true);
    }
    private void Finish()
    {
        FreezeMovability();
        movable = false;
        animator.SetBool("Movable", false);
        animator.SetBool("Ground", true);
        animator.SetFloat("Speed", 0f);
        animator.SetFloat("VerticalSpeed", 0);
        finished = true;
    }
}
