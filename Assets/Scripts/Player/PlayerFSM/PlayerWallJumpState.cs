using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : IState
{
    private PlayerController player;
    private const float jumpTimer= 0.25f;
    private float curTimer;
    public PlayerWallJumpState(PlayerController player)
    {
        this.player = player;
        curTimer = 0f;
    }
    public void Enter()
    {
        if (curTimer > jumpTimer)
        {
            WallJump();
            curTimer = 0f;
        }
        Debug.Log("wall jump state");
    }

    public void Exit()
    {
        curTimer = 0f;
    }

    public void FixedUpdate()
    {

    }

    public void Update()
    {
        if (curTimer < jumpTimer) curTimer += Time.deltaTime;
        Debug.Log(curTimer);
    }
    private void WallJump()
    {
        float jumpForce = player.jumpForce;
        float horizontalJumpWeight = 1.5f;
        Vector2 jumpDir = Vector2.up;

        if (player.render.flipX == false) jumpDir.x = horizontalJumpWeight;
        else if (player.render.flipX == true) jumpDir.x = -horizontalJumpWeight;
        jumpDir = jumpDir.normalized;

        player.rigid.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
    }
    private void Jump()
    {
        float jumpForce = 20f;
        player.rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
