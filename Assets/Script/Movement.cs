using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerControls controls;

    public Rigidbody2D rb;
    public float speed;
    private float moveInput;
    private float targetSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    public float frictionAmount;
    public float jumpForce;
    public float gravityDown;

    private bool isGrounded = false;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Right.performed += ctx => Right();
        controls.Gameplay.Right.canceled += ctx => moveInput = 0f;

        controls.Gameplay.Left.performed += ctx => Left();
        controls.Gameplay.Left.canceled += ctx => moveInput = 0f;

        controls.Gameplay.Jump.performed += ctx => Up();

    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Right();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Left();
        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Up();
        }
        if (rb.velocity.y < 0.2 && !isGrounded)
        {
            rb.gravityScale = gravityDown;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    void FixedUpdate()
    {
           
        float targetSpeed = moveInput * speed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif)*accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
        amount *= Mathf.Sign(rb.velocity.x);
        rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);

        //Jump
       
    }
    private void Jump()
    {
        isGrounded = false;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
        }
    }
    private void Right()
    {
        Debug.Log("caca");
        moveInput = 1f;
    }
    private void Left()
    {
        moveInput = -1f;
    }
    private void Up()
    {
        if (isGrounded)
        {
            Jump();
        }
    }
}
