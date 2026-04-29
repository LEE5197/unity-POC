using UnityEngine;

public class PlayerJumpState : IState
{
	private PlayerController player;
	public PlayerJumpState(PlayerController player) => this.player = player;
	public void Enter()
	{
		Jump();
		Debug.Log("Run Jump Enter");
	}

	public void Exit()
	{
		Debug.Log("Run Jump Exit");
	}

	public void Update()
	{
		
	}

	public void FixedUpdate()
	{
		Move();
	}

	private void Jump()
	{
		player.rigid.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
	}

    private void Move()
    {
        float moveSpeed = Mathf.Lerp(player.rigid.velocity.x, player.moveDir.x * player.moveSpeed, Time.deltaTime * 15f);

        player.rigid.velocity = new Vector2(moveSpeed, player.rigid.velocity.y);
    }
}
