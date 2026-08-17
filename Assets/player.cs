using System;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {

    // }
    private Animator animate;
    private Rigidbody2D rb;
    public Collider2D[] colliders;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 5f;
    [Header("Attack")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackpoint;
    [SerializeField] private LayerMask whatIsEnemy;
    private float xInput;
    private float yInput;
    private bool isFacingRight = true;
    private bool isCanMove = true;
    private bool isCanJump = true;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        HandleMovement();
        handleInput();
        handleAnimation();

    }

    private void handleAnimation()
    {
        animate.SetFloat("xVelocity", rb.linearVelocity.x);
        animate.SetFloat("yVelocity", rb.linearVelocity.y);
        animate.SetBool("isJumping", rb.linearVelocity.y != 0);
    }
    private void handleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && rb.linearVelocity.y == 0)
            Jump();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Attack();
    }

    public void DamageEnemies()
    {
       Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackpoint.position, attackRadius, whatIsEnemy);
       
       foreach (Collider2D enemy in enemyColliders)
       {
            enemy.GetComponent<Enemy>().TakeDamage();
       }
    }

    private void Attack()
    {
        if (rb.linearVelocity.y == 0)
        {
            animate.SetTrigger("Attack");
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
    }

    private void HandleMovement()
    {
        if (isCanMove)
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);
            handleFlip();
        }
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
    }

    private void Jump()
    {
        if (isCanJump)
            rb.linearVelocityY = jumpForce;
    }

    private void handleFlip()
    {
        if (rb.linearVelocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (rb.linearVelocity.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    // [ContextMenu("Flip")]
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    public void setIsCanMove(bool data)
    {
        isCanMove = data;
    }

    public void setIsCanJump(bool data)
    {
        isCanJump = data;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackpoint.position, attackRadius);
    }
    
}

