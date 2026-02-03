using System;
using UnityEngine;

public class Player1Move : TeamBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform model;
    [SerializeField] private Animator anim;

    private bool isGrounded;
    private float lastVelocityY;

    public readonly int IsRunHash = Animator.StringToHash("isRun");
    public readonly int JumpHash = Animator.StringToHash("Jump");
    public readonly int animSpeedYHash = Animator.StringToHash("animSpeedY");

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (model == null)
            model = transform.Find("Model");
        if (anim == null && model != null)
            anim = model.GetComponent<Animator>();
    }

    private void Update()
    {
        // horizontalInput = Input.GetAxis("Horizontal");

        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     Jump();
        // }
        float inputX = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(inputX) < 0.1f)
        {
            anim.SetBool(IsRunHash, false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        if (Mathf.Abs(rb.velocity.y) > 0.1f)
        {
            isGrounded = false;
        }
        else if (Mathf.Abs(rb.velocity.y) <= 0.1f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (lastVelocityY == rb.velocity.y)
        {
            return;
        }
        anim.SetFloat(animSpeedYHash, rb.velocity.y);
        lastVelocityY = rb.velocity.y;
    }

    public void PerformMove(float direction)
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        anim.SetBool(IsRunHash, Mathf.Abs(direction) > 0.01f);

        HandleFlip(direction);
    }

    public void PerformJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger(JumpHash);
        }
    }

    private void HandleFlip(float direction)
    {
        if (direction < 0.01f && direction > -0.01f) return;

        Vector3 localScale = model.localScale;
        if (direction > 0.01f)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        else if (direction < -0.01f)
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        model.localScale = localScale;
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }
}
