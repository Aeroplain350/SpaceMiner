using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;

    public float ballSpeed = 25f;

    public bool moveable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveable = false;
    }

    public void StartMovement()
    {
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), 1f).normalized * ballSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * ballSpeed;
    }
}
