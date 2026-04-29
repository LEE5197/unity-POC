using UnityEngine;

public class PlayerIdleState : IState
{
	PlayerController player;
	public PlayerIdleState(PlayerController player)
	{
		this.player = player;
	}
	public void Enter()
	{
		player.anim.Play("idle");
		Debug.Log("Idle State Enter");
	}

	public void Exit()
	{
		Debug.Log("Idle State Exit");
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
