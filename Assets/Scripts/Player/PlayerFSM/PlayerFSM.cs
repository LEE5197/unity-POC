using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM
{
    private StateMachine<PlayerController> fsm;
	public BaseState<PlayerController> curState { get; private set; }
	public BaseState<PlayerController> prevState { get; private set; }

    public PlayerFSM(PlayerController player)
	{
		fsm = new StateMachine<PlayerController>(player);
		InitStateDic(player);

		ChangeState("idle");
	}

	private void InitStateDic(PlayerController player)
	{
		fsm.AddState("idle", new IdleState(player));
		fsm.AddState("run", new RunState(player));
		fsm.AddState("jump", new JumpState(player));
		fsm.AddState("fall", new FallState(player));
		fsm.AddState("wallGrab", new WallGrapState(player));
		fsm.AddState("wallClimb", new WallClimbState(player));
		fsm.AddState("wallSlide", new WallSlideState(player));
	}

	public void ChangeState(string key)
	{
		fsm.ChangeState(key);
		prevState = fsm.prevState;
		curState = fsm.curState;
		//Debug.Log($"previous state ${prevState}");
		Debug.Log($"current state : {curState}");
	}

	public void Update()
	{
		fsm.Update();
	}

	public void FixedUpdate()
	{
		fsm.FixedUpdate();
	}

	public BaseState<PlayerController> GetState(string key)
	{
		return fsm.stateDic[key];
	}
}
