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

		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<PlayerCollision>();
		render = GetComponent<SpriteRenderer>();

        playerFSM.changeState(idleState);
	}

	private void Update()
	{
		if (coll.onGround && moveDir.x == 0 && !isJump) playerFSM.changeState(idleState);
		else if (coll.onGround && moveDir.x != 0 && !isJump) playerFSM.changeState(moveState);
		else if (coll.onGround && isJump) playerFSM.changeState(jumpState);
		playerFSM.curstate.Update();
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
