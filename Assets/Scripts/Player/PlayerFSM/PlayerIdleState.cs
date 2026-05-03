using UnityEngine;

public class PlayerIdleState : IState
{
	PlayerController player;
	private int anim = Animator.StringToHash("idle");
	public PlayerIdleState(PlayerController player)
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

	public void Update()
	{
		
	}

	public void FixedUpdate()
	{
		Move();
	}

    private void Move()
    {
        float moveSpeed = Mathf.Lerp(player.rigid.velocity.x, player.moveDir.x * player.moveSpeed, Time.deltaTime * 20f);

        player.rigid.velocity = new Vector2(moveSpeed, player.rigid.velocity.y);
    }
}
