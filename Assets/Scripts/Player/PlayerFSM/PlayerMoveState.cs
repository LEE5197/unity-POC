using UnityEngine;

public class PlayerMoveState : IState
{
	private PlayerController player;
	public PlayerMoveState(PlayerController player) => this.player = player;
	public void Enter()
	{
		Debug.Log("Run State Enter");
	}

	public void Exit()
	{
		Debug.Log("Run State Exit");
	}

	public void Update()
	{

	}

	public void FixedUpdate()
	{

	}
}