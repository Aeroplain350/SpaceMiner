using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movement;
    public float paddleSpeed = 2f;

    public float maxBounceAngle = 75f;

    public bool moveable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveable = false;
    }

    public void OnMovement(InputValue input)
    {
        if (!moveable) return;

        movement = input.Get<float>();

        rb.velocity = new Vector2(movement * paddleSpeed, rb.velocity.y);
    }

    public void OnPressedMovementButton(float input)
    {
        if (!moveable) return;

        rb.velocity = new Vector2(input * paddleSpeed, rb.velocity.y);
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector2 paddlePosition = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float maxOffset = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);
            float bounceAngle = (offset / maxOffset) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rb.velocity = rotation * Vector2.up * ball.rb.velocity.magnitude;
        }
    }
}
