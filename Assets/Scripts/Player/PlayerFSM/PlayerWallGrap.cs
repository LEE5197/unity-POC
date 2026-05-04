using UnityEngine;

public class PlayerWallGrap : IState
{
    private PlayerController player;
    private int wallGrapAnim = Animator.StringToHash("wallGrap");
    private int wallSlideAnim = Animator.StringToHash("wallSlide");
    private int wallClimbAnim = Animator.StringToHash("wallClimb");
    
    private enum curAnimState { None, Grap, Slide, Move};
    private curAnimState curAnim;

    public bool canJump { get; private set; }

    public PlayerWallGrap(PlayerController player)
    {
        this.player = player;
        canJump = false;
    }
    public void Enter()
    {
        player.anim.Play(wallGrapAnim);
        player.rigid.gravityScale = 0f;
        curAnim = curAnimState.Grap;
        if (player.moveDir.x == 1) player.render.flipX = false;
        else if (player.moveDir.x == -1) player.render.flipX = true;
        canJump = true;
    }

    public void Exit()
    {
        player.rigid.gravityScale = 1f;
        curAnim = curAnimState.None;
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void Update()
    {
        if (player.moveDir.y == 0 && curAnim != curAnimState.Grap)
        {
            player.anim.Play(wallGrapAnim);
            curAnim = curAnimState.Grap;
        }
        else if (player.moveDir.y == -1 && curAnim != curAnimState.Slide)
        {
            player.anim.Play(wallSlideAnim);
            curAnim = curAnimState.Slide;
        }
        else if (player.moveDir.y == 1 && curAnim != curAnimState.Move)
        {
            player.anim.Play(wallClimbAnim);
            curAnim = curAnimState.Move;
        }

    }

    private void Move()
    {
        float moveSpeedHorizontal = Mathf.Lerp(player.rigid.velocity.x, player.moveDir.x * player.moveSpeed, Time.deltaTime * 15f);
        float moveSpeedVertical = player.moveSpeed * 0.8f * player.moveDir.y;
        player.rigid.velocity = new Vector2(moveSpeedHorizontal, moveSpeedVertical);
    }

}
