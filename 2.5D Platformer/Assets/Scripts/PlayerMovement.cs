using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        Standing,
        Walking,
        Running,
        Jumping,
        Falling
    }

    public enum MovementMode
    {
        SideView,
        TopDownView,
        ThirdPerson
    }

    [Header("Movement")]
    public float jumpForce = 3f;
    public float walkingSpeed = 4f;
    public float runningSpeed = 8f;
    public float stamina = 20;
    public float staminaRegenSec = 1;
    public float staminaUsedSec = 1;
    public PlayerState playerState = PlayerState.Standing;
    public static bool CanMove = true;
    public float mouseSensitivity = 7;
    [Space]
    [Header("Movement Mode")]
    [SerializeField] private MovementMode movementMode;
    private PlayerSideViewMovement playerSideViewMovement;
    private PlayerTopDownMovement playerTopDownMovement;
    private PlayerThirdPersonMovement playerThirdPersonMovement;
    [Space]
    [Header("GroundCheck")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundCheckHeight = -0.505f;
    [SerializeField] private Vector3 boxSize = new Vector3(0.6f, 0.02f, 0.6f);
    [Space]

    private Rigidbody rigidbody;
    private float maxStamina;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        playerSideViewMovement = this.GetComponent<PlayerSideViewMovement>();
        playerTopDownMovement = this.GetComponent<PlayerTopDownMovement>();
        playerThirdPersonMovement = this.GetComponent<PlayerThirdPersonMovement>();
        CanMove = true;
        maxStamina = stamina;
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
                case MovementMode.ThirdPerson:
                    if (playerThirdPersonMovement)
                    {
                        playerThirdPersonMovement.Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                    }
                    break;
                default:
                    break;
            }
            Stamina();
        }
    }

    private void FixedUpdate()
    {
        State();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position + new Vector3(0, groundCheckHeight, 0), boxSize * 2);
    }

    private void State()
    {
        if (GroundCheck())
        {
            if (rigidbody.velocity.x != 0f && (rigidbody.velocity.x <= walkingSpeed && rigidbody.velocity.x >= walkingSpeed * -1))
            {
                playerState = PlayerState.Walking;
            }
            else if (rigidbody.velocity.x != 0f && (rigidbody.velocity.x <= runningSpeed && rigidbody.velocity.x >= runningSpeed * -1))
            {
                playerState = PlayerState.Running;
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

    private void Stamina()
    {
        switch (playerState)
        {
            case PlayerState.Standing:
                stamina = Mathf.Clamp(stamina + Time.deltaTime * staminaRegenSec, 0, maxStamina);
                break;
            case PlayerState.Walking:
                stamina = Mathf.Clamp(stamina + Time.deltaTime * staminaRegenSec / 2, 0, maxStamina);
                break;
            case PlayerState.Running:
                stamina = Mathf.Clamp(stamina - Time.deltaTime * staminaUsedSec, 0, maxStamina);
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Falling:
                break;
            default:
                break;
        }
    }

    private bool GroundCheck()
    {
        return Physics.CheckBox(transform.position + new Vector3(0, groundCheckHeight, 0), boxSize, new Quaternion(0, 0, 0, 0), layerMask);
    }
}
