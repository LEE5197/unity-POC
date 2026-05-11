using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public LayerMask groundLayer;

    public bool onGround;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    public Vector2 bottomOffset, rightOffset, leftOffset;
    public Vector2 bottomBoxSize, sideBoxSize;
 
    void Update()
    {
        onGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, bottomBoxSize, 0f, groundLayer);

        onRightWall = Physics2D.OverlapBox((Vector2)transform.position + rightOffset, sideBoxSize, 0f, groundLayer);
        onLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftOffset, sideBoxSize, 0f, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, bottomBoxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + rightOffset, sideBoxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + leftOffset, sideBoxSize);
    }
}
