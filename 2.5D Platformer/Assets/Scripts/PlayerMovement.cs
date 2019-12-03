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

    public enum MovementMode
    {
        SideView,
        TopDownView
    }

    [Header("Movement")]
    public float jumpForce = 3f;
    public float walkingSpeed = 0.5f;
    public PlayerState playerState = PlayerState.Standing;

    [Header("Movement Mode")]
    [SerializeField] private MovementMode movementMode;
    private PlayerSideViewMovement playerSideViewMovement;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundCheckHeight = -0.505f;
    [SerializeField] private Vector3 boxSize = new Vector3(0.6f, 0.02f, 0.6f);

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        playerSideViewMovement = this.GetComponent<PlayerSideViewMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (movementMode)
        {
            case MovementMode.SideView:
                if (playerSideViewMovement)
                {
                    playerSideViewMovement.Movement();
                }
                break;
            case MovementMode.TopDownView:
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        State();
    }

    private void Move(float speed)
    {
        rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y,0);
    }

    public void Jump()
    {
        playerState = PlayerState.Jumping;
        rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position + new Vector3(0, groundCheckHeight, 0), boxSize);
    }

    private void State()
    {
        if (GroundCheck())
        {
            if (rigidbody.velocity.x != 0f)
            {
                playerState = PlayerState.Walking;
            }
            else
            {
                playerState = PlayerState.Standing;
            }
        }
        else if (rigidbody.velocity.y < 0f)
        {
            playerState = PlayerState.Falling;
        }
        else
        {
            playerState = PlayerState.Jumping;
        }
    }

    private void Movement()
    {
        if (PlayerHealth.PlayerAlive)
        {
            switch (playerState)
            {
                case PlayerState.Standing:
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Jump();
                    }
                    break;
                case PlayerState.Walking:
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Jump();
                    }
                    break;
                case PlayerState.Jumping:
                    break;
                case PlayerState.Falling:
                    break;
                default:
                    break;
            }
            Move(walkingSpeed * Input.GetAxis("Horizontal"));
        }
    }

    private bool GroundCheck()
    {
        return Physics.CheckBox(transform.position + new Vector3(0, groundCheckHeight, 0), boxSize, new Quaternion(0, 0, 0, 0), layerMask);
    }
}
