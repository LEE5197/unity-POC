using UnityEngine;

public class PlayerFallState : IState
{
    private PlayerController player;
    private int anim = Animator.StringToHash("fall");

    public PlayerFallState(PlayerController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.anim.Play(anim);
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void Update()
    {
    }
    private void Move()
    {
        float moveSpeed = Mathf.Lerp(player.rigid.velocity.x, player.moveDir.x * player.moveSpeed, Time.deltaTime * 15f);

        player.rigid.velocity = new Vector2(moveSpeed, player.rigid.velocity.y);
    }
}
