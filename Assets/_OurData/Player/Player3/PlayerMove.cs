using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : TeamBehaviour
{
    [Header("References")]
    [SerializeField] private Transform body;
    [Header("Player Move Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private Transform armTransform;

    [Header("Rotation Limits")]
    [SerializeField] private float minAngle = -50f;
    [SerializeField] private float maxAngle = 40f;
    // ĐÃ XÓA: rotationOffset

    [Header("Physics Components")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Sensor groundCheck;
    [SerializeField] private Animator _animator;

    private Camera _mainCamera;
    private float _horizontalInput;

    public readonly int IsRunningHash = Animator.StringToHash("isRunning");
    // public readonly int IsJumpingHash = Animator.StringToHash("isJumping");
    public readonly int InputXHash = Animator.StringToHash("InputX");

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (body == null)
            body = transform.Find("Body");
        if (armTransform == null)
            armTransform = body.Find("RightArm");

        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();

        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();

        if (groundCheck == null)
            groundCheck = transform.Find("Detector")?.Find("GroundCheck")?.GetComponent<Sensor>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        this.RotateArmToMouse();

        if (Input.GetButtonDown("Jump") && groundCheck != null && groundCheck.IsDetected)
        {
            Jump();
        }

        UpdateAnimation();
        this.Turning();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_horizontalInput * moveSpeed, _rb.velocity.y);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void UpdateAnimation()
    {
        if (_animator == null) return;

        bool isMoving = Mathf.Abs(_horizontalInput) > 0.1f;
        _animator.SetBool(IsRunningHash, isMoving);
        float animSpeed = 1f;
        if (_horizontalInput < -0.01f)
        {
            animSpeed = -1f;
        }

        _animator.SetFloat(InputXHash, animSpeed * body.localScale.x);
    }

    private void RotateArmToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 direction = mousePos - armTransform.position;

        if (body.localScale.x < 0)
        {
            direction.x = -direction.x;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        armTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    protected virtual void Turning()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 vec3 = new Vector3(mouse.x, mouse.y, this.body.transform.position.y);
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(vec3);
        Vector3 mouseToChar = mouseWorld - this.body.transform.position;
        body.transform.localScale = new Vector3(Mathf.Sign(mouseToChar.x), 1, 1);
    }
}