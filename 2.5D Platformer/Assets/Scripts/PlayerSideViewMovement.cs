using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideViewMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Rigidbody rigidbody;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(float input)
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        switch (playerMovement.playerState)
        {
            case PlayerMovement.PlayerState.Standing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Jump();
                }
                break;
            case PlayerMovement.PlayerState.Walking:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Jump();
                }
                break;
            case PlayerMovement.PlayerState.Running:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Jump();
                }
                break;
            case PlayerMovement.PlayerState.Jumping:
                break;
            case PlayerMovement.PlayerState.Falling:
                break;
            default:
                break;
        }
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && playerMovement.stamina > 0)
        {
            Move(playerMovement.runningSpeed * input);
        }
        else
        {
            Move(playerMovement.walkingSpeed * input);
        }
    }

    private void Move(float speed)
    {
        rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y, 0);
    }

    public void Jump()
    {
        if (PlayerMovement.CanMove)
        {
            audioSource.PlayOneShot(jumpSound);
            playerMovement.playerState = PlayerMovement.PlayerState.Jumping;
            rigidbody.AddForce(new Vector2(0, playerMovement.jumpForce), ForceMode.Impulse);
        }

    }
}
