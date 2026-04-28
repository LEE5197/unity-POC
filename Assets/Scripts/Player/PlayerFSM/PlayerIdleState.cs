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

	}
}
