using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private PlayerFSM playerFSM = new PlayerFSM();
	private PlayerIdleState idleState;
	private PlayerMoveState moveState;
	private PlayerJumpState jumpState;
	private Animator anim;

	private PlayerCollision coll;
	public Rigidbody2D rigid;
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
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
		playerFSM.changeState(idleState);

		coll = GetComponent<PlayerCollision>();
	}

	private void Update()
	{
		if (coll.onGround && moveDir.x == 0 && !isJump) playerFSM.changeState(idleState);
		else if (coll.onGround && moveDir.x != 0 && !isJump) playerFSM.changeState(moveState);
		else if (coll.onGround && isJump)
		{
			playerFSM.changeState(jumpState);
			Jump();
		}
		playerFSM.curstate.Update();
	}

	private void FixedUpdate()
	{
		Move();
		playerFSM.curstate.FixedUpdate();
	}

	private void OnMove(InputValue value) => moveDir = value.Get<Vector2>();
	private void OnJump(InputValue value)
	{
		isJump = value.isPressed;
		Debug.Log(isJump);
	}

	private void Move()
	{
		rigid.velocity = new Vector2(moveDir.x * moveSpeed, rigid.velocity.y);
	}

	private void Jump()
	{
		rigid.AddForce(Vector2.up * jumpForce);
	}
}
