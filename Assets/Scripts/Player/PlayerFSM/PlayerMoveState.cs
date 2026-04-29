using UnityEngine;

public class PlayerMoveState : IState
{
	private PlayerController player;
	public PlayerMoveState(PlayerController player) => this.player = player;
	public void Enter()
	{
		player.anim.Play("run");
		Debug.Log("Run State Enter");
	}

	public void Exit()
	{
		Debug.Log("Run State Exit");
	}

	public void Update()
	{
		if (player.moveDir.x == 1)
			player.render.flipX = false;
		else if (player.moveDir.x == -1)
			player.render.flipX = true;
	}

	public void FixedUpdate()
	{
		Move();
	}

    private void Move()
    {
        float moveSpeed = Mathf.Lerp(player.rigid.velocity.x, player.moveDir.x * player.moveSpeed, Time.deltaTime * 15f);

        player.rigid.velocity = new Vector2(moveSpeed, player.rigid.velocity.y);
    }
}