using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = -moveSpeed;
        rb.linearVelocity = velocity;

    }
}
