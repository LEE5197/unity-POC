using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public PlayerFSM playerFSM { get; private set; } = new PlayerFSM();
	private PlayerIdleState idleState;
	private PlayerMoveState moveState;
	private PlayerJumpState jumpState;
	private PlayerFallState fallState;
	public PlayerWallJumpState wallJumpState { get; private set; }
	public PlayerWallGrap grapState { get; private set; }

    private PlayerCollision coll;
    
	public Animator anim { get; private set; }
	public Rigidbody2D rigid { get; private set; }
	public SpriteRenderer render { get; private set; }
    public Vector2 moveDir { get; private set; }
	public bool isJump { get; private set; }

    [Range(0f, 30f)]
    public float moveSpeed;
	[Range(0f, 100f)]
	public float jumpForce = 5f;

    void Awake()
    {
		idleState = new PlayerIdleState(this);
		moveState = new PlayerMoveState(this);
		jumpState = new PlayerJumpState(this);
		fallState = new PlayerFallState(this);
		grapState = new PlayerWallGrap(this);
		wallJumpState = new PlayerWallJumpState(this);

		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<PlayerCollision>();
		render = GetComponent<SpriteRenderer>();

        playerFSM.changeState(idleState);
	}

	private void Update()
	{
        playerFSM.curstate.Update();

		//Enter wall grap state
		if ((coll.onLeftWall && moveDir.x == -1) || (coll.onRightWall && moveDir.x == 1)) playerFSM.changeState(grapState);
		//Exit wall grap state
		if (playerFSM.curstate == grapState && isJump) playerFSM.changeState(wallJumpState);
		else if (playerFSM.curstate == grapState && ((!render.flipX && !coll.onRightWall) || (render.flipX && !coll.onLeftWall))) playerFSM.changeState(idleState);
		

		if (playerFSM.curstate == grapState) return;

		if (coll.onGround && moveDir.x == 0 && !isJump) playerFSM.changeState(idleState);
		else if (coll.onGround && moveDir.x != 0 && !isJump) playerFSM.changeState(moveState);
		else if (coll.onGround && isJump) playerFSM.changeState(jumpState);
		else if (!coll.onGround && rigid.velocity.y < 0) playerFSM.changeState(fallState);

	}

	private void FixedUpdate()
	{
		playerFSM.curstate.FixedUpdate();
	}

	private void OnMove(InputValue value) => moveDir = value.Get<Vector2>();
	private void OnJump(InputValue value)
	{
		isJump = value.isPressed;
		Debug.Log(isJump);
	}

}
