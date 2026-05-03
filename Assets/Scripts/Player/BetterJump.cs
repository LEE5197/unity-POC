using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;
    public float JumpMultiplier = 2f;

    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = player.rigid;
    }

    void Update()
    {
        if (rb.velocity.y < 0 || (rb.velocity.y > 0 && !player.isJump))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && player.isJump)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * JumpMultiplier * Time.deltaTime;
        }
    }
}
