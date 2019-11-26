using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        Running,
        Standing,
        Walking,
        Jumping,
        Falling
    }

    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private LayerMask layerMask;
    public PlayerState playerState = PlayerState.Standing;

    private Rigidbody2D rigidbody2D;

    [SerializeField] private float runningSpeed = 1f;
    [SerializeField] private float walkingSpeed = 0.5f;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            Move(walkingSpeed * Input.GetAxis("Horizontal"));
            if (Physics2D.OverlapBox((Vector2)transform.position + new Vector2(0, -0.505f), new Vector2(0.2f, 0.02f), 0, layerMask) && playerState != PlayerState.Jumping)
            {
                if (rigidbody2D.velocity.x <= walkingSpeed && rigidbody2D.velocity.x > 0.1f)
                {
                    playerState = PlayerState.Walking;
                }
                else if (rigidbody2D.velocity.x <= runningSpeed && rigidbody2D.velocity.x > 0.1f)
                {
                    playerState = PlayerState.Running;
                }
                else
                {
                    playerState = PlayerState.Standing;
                }
            }
            else if (rigidbody2D.velocity.y < -0.1f)
            {
                playerState = PlayerState.Falling;
            }
    }

    private void Move(float speed)
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    public void Jump()
    {
            if (playerState != PlayerState.Jumping && playerState != PlayerState.Falling)
            {
                rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                playerState = PlayerState.Jumping;
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube((Vector2)transform.position + new Vector2(0, -0.505f), new Vector2(0.2f, 0.02f));
    }
}
