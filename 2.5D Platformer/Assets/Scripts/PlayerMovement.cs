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
    public static bool CanMove = true;

    [Header("Movement Mode")]
    [SerializeField] private MovementMode movementMode;
    private PlayerSideViewMovement playerSideViewMovement;
    private PlayerTopDownMovement playerTopDownMovement;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundCheckHeight = -0.505f;
    [SerializeField] private Vector3 boxSize = new Vector3(0.6f, 0.02f, 0.6f);

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        playerSideViewMovement = this.GetComponent<PlayerSideViewMovement>();
        playerTopDownMovement = this.GetComponent<PlayerTopDownMovement>();
        CanMove = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (CanMove)
        {
            switch (movementMode)
            {
                case MovementMode.SideView:
                    if (playerSideViewMovement)
                    {
                        playerSideViewMovement.Movement(Input.GetAxis("Horizontal"));
                    }
                    break;
                case MovementMode.TopDownView:
                    if (playerTopDownMovement)
                    {
                        playerTopDownMovement.Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        State();
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

    private bool GroundCheck()
    {
        return Physics.CheckBox(transform.position + new Vector3(0, groundCheckHeight, 0), boxSize, new Quaternion(0, 0, 0, 0), layerMask);
    }
}
