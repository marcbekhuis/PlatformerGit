using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(float inputX, float inputZ)
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        switch (playerMovement.playerState)
        {
            case PlayerMovement.PlayerState.Standing:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                break;
            case PlayerMovement.PlayerState.Walking:
                if (Input.GetKeyDown(KeyCode.Space))
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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Move(playerMovement.runningSpeed, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            Move(playerMovement.walkingSpeed, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    private void Move(float speed, float inputX, float inputZ)
    {
        Vector2 speed2 = new Vector2(inputX, inputZ);
        speed2.Normalize();
        speed2 *= speed;

        rigidbody.velocity = new Vector3(speed2.x, rigidbody.velocity.y, speed2.y);
    }

    public void Jump()
    {
        playerMovement.playerState = PlayerMovement.PlayerState.Jumping;
        rigidbody.AddForce(new Vector2(0, playerMovement.jumpForce), ForceMode.Impulse);

    }
}
