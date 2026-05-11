using UnityEngine;

public class IdleState : BaseState<PlayerController>
{
	public IdleState(PlayerController player) : base("idle", player) { }
	public override void Enter()
	{
		target.anim.Play(anim);
	}
	public override void Update()
	{
		if (target.isJump && target.coll.onGround&&target.canJump) target.fsm.ChangeState("jump");
		else if (target.rigid.velocity.y > 0 && target.coll.onGround) target.fsm.ChangeState("jump");
		if (target.moveDir.x != 0 && target.coll.onGround) target.fsm.ChangeState("run");
	}
	public override void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		float moveSpeed = Mathf.Lerp(target.rigid.velocity.x, target.moveDir.x * target.moveSpeed, Time.deltaTime * 20f);
		target.rigid.velocity = new Vector2(moveSpeed, target.rigid.velocity.y);
	}
}

public class RunState : BaseState<PlayerController>
{
	public RunState(PlayerController player) : base("run", player) { }

	public override void Enter()
	{
		target.anim.Play(anim);
		SetSpriteDir();
	}
	public override void Update()
	{
		SetSpriteDir();

		if (target.moveDir.x == 0 && target.coll.onGround) target.fsm.ChangeState("idle");

		if (target.isJump && target.coll.onGround) target.fsm.ChangeState("jump");
		else if (target.rigid.velocity.y > 0 && target.coll.onGround) target.fsm.ChangeState("jump");

		if (target.rigid.velocity.y < 0 && !target.coll.onGround) target.fsm.ChangeState("fall");
	}
	public override void FixedUpdate()
	{
		Move();	
	}
	private void Move()
	{
		float moveSpeed = Mathf.Lerp(target.rigid.velocity.x, target.moveDir.x * target.moveSpeed, Time.deltaTime * 20f);
		target.rigid.velocity = new Vector2(moveSpeed, target.rigid.velocity.y);
	}
	private void SetSpriteDir()
	{
		if (target.moveDir.x == 1) target.render.flipX = false;
		if (target.moveDir.x == -1) target.render.flipX = true;
	}
}

public class JumpState : BaseState<PlayerController>
{
	public JumpState(PlayerController player) : base("jump", player) { }
	public override void Enter()
	{
		target.anim.Play(anim);
		if (target.canJump)
		{
			Jump();
		}
	}
	public override void Update()
	{
		if (target.coll.onGround&&target.canJump) target.fsm.ChangeState("idle");
		if (!target.coll.onGround && target.rigid.velocity.y < 0) target.fsm.ChangeState("fall");
		if (target.coll.onRightWall && target.moveDir.x == 1) target.fsm.ChangeState("wallGrab");
		if (target.coll.onLeftWall && target.moveDir.x == -1) target.fsm.ChangeState("wallGrab");
	}
	public override void FixedUpdate()
	{
		Move();
	}

	private void Jump()
	{
		if (!target.coll.onGround || !target.canJump) return;

		target.CanJumpTimer();
		target.rigid.AddForce(Vector2.up * target.jumpForce, ForceMode2D.Impulse);
		
	}
	private void Move()
	{
		float moveSpeed = Mathf.Lerp(target.rigid.velocity.x, target.moveDir.x * target.moveSpeed, Time.deltaTime * 20f);
		target.rigid.velocity = new Vector2(moveSpeed, target.rigid.velocity.y);
	}
}

public class FallState : BaseState<PlayerController>
{
	public FallState(PlayerController player) : base("fall", player) { }
	public override void Enter()
	{
		target.anim.Play(anim);
	}
	public override void Update()
	{
		if (target.coll.onGround) target.fsm.ChangeState("idle");
		if (target.coll.onRightWall && target.moveDir.x == 1) target.fsm.ChangeState("wallGrab");
		if (target.coll.onLeftWall && target.moveDir.x == -1) target.fsm.ChangeState("wallGrab");
	}
	public override void FixedUpdate()
	{
		Move();
	}
	private void Move()
	{
		float moveSpeed = Mathf.Lerp(target.rigid.velocity.x, target.moveDir.x * target.moveSpeed, Time.deltaTime * 20f);
		target.rigid.velocity = new Vector2(moveSpeed, target.rigid.velocity.y);
	}
}

public class WallGrapState : BaseState<PlayerController>
{
	public WallGrapState(PlayerController player) : base("wallGrab",player) { }
	public override void Enter()
	{
		target.anim.Play(anim);
		SetEnter();

	}
	public override void Update()
	{
		if (target.moveDir.y == 1) target.fsm.ChangeState("wallClimb");
		if (target.moveDir.y == -1) target.fsm.ChangeState("wallSlide");
		
		if (target.isJump && target.moveDir.x == 0) target.fsm.ChangeState("fall");
		if (!target.coll.onRightWall && !target.render.flipX) target.fsm.ChangeState("fall");
		if (!target.coll.onLeftWall && target.render.flipX) target.fsm.ChangeState("fall");

		if (target.isJump && target.moveDir.x != 0) WallJump();


	}

	public override void Exit()
	{
		SetExit();
	}
	private void WallJump()
	{
		if (!target.canJump) return;
		if (target.coll.onRightWall && target.moveDir.x == 1) return;
		if (target.coll.onLeftWall && target.moveDir.x == -1) return;

		target.CanJumpTimer();

		Vector2 jumpDir = new Vector2(0f, 1f);

		if (target.coll.onRightWall && target.moveDir.x == -1f)
		{
			jumpDir = new Vector2(-1f, 1f);
		}
		else if (target.coll.onLeftWall && target.moveDir.x == 1f)
		{
			jumpDir = new Vector2(1f, 1f);
		}
		jumpDir = jumpDir.normalized;

		target.rigid.AddForce(jumpDir * target.wallJumpForce, ForceMode2D.Impulse);
		target.fsm.ChangeState("jump");

		if (target.moveDir.x == 1) target.render.flipX = false;
		else if (target.moveDir.x == -1) target.render.flipX = true;
	}
	private void SetEnter()
	{
		Vector2 pos = target.transform.position;
		target.rigid.gravityScale = 0f;
		target.rigid.velocity = Vector2.zero;

		if (target.fsm.prevState is WallClimbState || target.fsm.prevState is WallSlideState) return;
		if (target.moveDir.x == 1)
		{
			target.render.flipX = false;
			pos.x += 0.2f;
			target.transform.position = pos;
		}
		if (target.moveDir.x == -1)
		{
			target.render.flipX = true;
			pos.x -= 0.2f;
			target.transform.position = pos;
		}
	}
	private void SetExit()
	{
		target.rigid.gravityScale = 1f;

	}
}

public class WallClimbState : BaseState<PlayerController>
{
	public WallClimbState(PlayerController player) : base("wallClimb", player) { }
	public override void Enter()
	{
		target.anim.Play(anim);
		target.rigid.gravityScale = 0f;
	}
	public override void Update()
	{
		if (target.moveDir.y == 0) target.fsm.ChangeState("wallGrab");
		if (target.moveDir.y == -1) target.fsm.ChangeState("wallSlide");

		if (target.isJump && target.moveDir.x != 0) WallJump();
	}
	public override void FixedUpdate()
	{
		Move();
	}
	public override void Exit()
	{
		SetExit();
	}
	private void Move()
	{
		float moveSpeed = Mathf.Lerp(target.rigid.velocity.y, target.moveDir.y * target.moveSpeed * 1f, Time.deltaTime * 15f);
		target.rigid.velocity = new Vector2(0, moveSpeed);
	}
	private void SetExit()
	{
		target.rigid.gravityScale = 1f;
	}
	private void WallJump()
	{
		if (!target.canJump) return;
		if (target.coll.onRightWall && target.moveDir.x == 1) return;
		if (target.coll.onLeftWall && target.moveDir.x == -1) return;

		target.CanJumpTimer();

		Vector2 jumpDir = new Vector2(0f, 1f);

		if (target.coll.onRightWall && target.moveDir.x == -1f)
		{
			jumpDir = new Vector2(-1f, 1f);
		}
		else if (target.coll.onLeftWall && target.moveDir.x == 1f)
		{
			jumpDir = new Vector2(1f, 1f);
		}
		jumpDir = jumpDir.normalized;

		target.rigid.AddForce(jumpDir * target.wallJumpForce, ForceMode2D.Impulse);
		target.fsm.ChangeState("jump");

		if (target.moveDir.x == 1) target.render.flipX = false;
		else if (target.moveDir.x == -1) target.render.flipX = true;
	}
}
public class WallSlideState : BaseState<PlayerController>
{
	public WallSlideState(PlayerController player) : base("wallSlide", player) { }
	public override void Enter()
	{
		target.anim.Play(anim);
		target.rigid.gravityScale = 0f;
	}
	public override void Update()
	{
		if (target.moveDir.y == 0) target.fsm.ChangeState("wallGrab");
		if (target.moveDir.y == 1) target.fsm.ChangeState("wallClimb");

		if (target.isJump && target.moveDir.x != 0) WallJump();
	}
	public override void FixedUpdate()
	{
		Move();
	}
	public override void Exit()
	{
		SetExit();
	}
	private void Move()
	{
		float moveSpeed = Mathf.Lerp(target.rigid.velocity.y, target.moveDir.y * target.moveSpeed * 1f, Time.deltaTime * 15f);
		target.rigid.velocity = new Vector2(0, moveSpeed);
	}
	private void SetExit()
	{
		target.rigid.gravityScale = 1f;
	}
	private void WallJump()
	{
		if (!target.canJump) return;
		if (target.coll.onRightWall && target.moveDir.x == 1) return;
		if (target.coll.onLeftWall && target.moveDir.x == -1) return;

		target.CanJumpTimer();

		Vector2 jumpDir = new Vector2(0f, 1f);

		if (target.coll.onRightWall && target.moveDir.x == -1f)
		{
			jumpDir = new Vector2(-1f, 1f);
		}
		else if (target.coll.onLeftWall && target.moveDir.x == 1f)
		{
			jumpDir = new Vector2(1f, 1f);
		}
		jumpDir = jumpDir.normalized;

		target.rigid.AddForce(jumpDir * target.wallJumpForce, ForceMode2D.Impulse);
		target.fsm.ChangeState("jump");

		if (target.moveDir.x == 1) target.render.flipX = false;
		else if (target.moveDir.x == -1) target.render.flipX = true;
	}
}