using UnityEngine;

public class PlayerJumpState : IState
{
	private PlayerController player;
	public PlayerJumpState(PlayerController player) => this.player = player;
	public void Enter()
	{
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

	}

}
