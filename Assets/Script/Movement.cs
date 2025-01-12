using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed;
    private float moveInput;
    private float targetSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    public float frictionAmount;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        float targetSpeed = moveInput * speed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif)*accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
        amount *= Mathf.Sign(rb.velocity.x);
        rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);

        
    }
}
