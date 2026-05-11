using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public PlayerFSM fsm { get; private set; }

    public PlayerCollision coll { get; private set; }
    
	public Animator anim { get; private set; }
	public Rigidbody2D rigid { get; private set; }
	public SpriteRenderer render { get; private set; }
    public Vector2 moveDir { get; private set; }
	public bool isJump { get; private set; }
	public bool canJump { get; private set; }

    [Range(0f, 30f)]
    public float moveSpeed;
	[Range(0f, 100f)]
	public float jumpForce = 5f;
	[Range(0f, 100f)]
	public float wallJumpForce = 20f;

    void Awake()
    {
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<PlayerCollision>();
		render = GetComponent<SpriteRenderer>();

		fsm = new PlayerFSM(this);
		canJump = true;
	}

	private void Update()
	{
		fsm.Update();
	}

	private void FixedUpdate()
	{
		fsm.FixedUpdate();
	}

	private void OnMove(InputValue value)
	{
		moveDir = value.Get<Vector2>();
	}
	private void OnJump(InputValue value)
	{
		isJump = value.isPressed;
	}
	public void CanJumpTimer()
	{
		canJump = false;
		StartCoroutine(SetTimer());
	}
	private IEnumerator SetTimer()
	{
		yield return new WaitForSeconds(0.3f);
		canJump = true;
	}
}
