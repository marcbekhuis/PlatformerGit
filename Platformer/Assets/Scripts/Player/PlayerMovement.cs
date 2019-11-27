using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        Standing,
        Walking,
        Jumping,
        Falling
    }

    [Header("Movement")]
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float walkingSpeed = 0.5f;

    [Header("Other")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private PlayerState playerState = PlayerState.Standing;
    public float groundCheckHeight = -0.505f;


    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerHealth.PlayerAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            Move(walkingSpeed * Input.GetAxis("Horizontal"));
            if (Physics2D.OverlapBox((Vector2)transform.position + new Vector2(0, groundCheckHeight), new Vector2(0.2f, 0.02f), 0, layerMask) && playerState != PlayerState.Jumping)
            {
                if (rigidbody2D.velocity.x <= walkingSpeed && rigidbody2D.velocity.x > 0.1f)
                {
                    playerState = PlayerState.Walking;
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
        else
        {
            rigidbody2D.bodyType = RigidbodyType2D.Static;
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
        Gizmos.DrawCube((Vector2)transform.position + new Vector2(0, groundCheckHeight), new Vector2(0.2f, 0.02f));
    }
}
