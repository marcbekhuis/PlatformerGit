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

    public void Movement()
    {
        if (PlayerHealth.PlayerAlive)
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
                case PlayerMovement.PlayerState.Jumping:
                    break;
                case PlayerMovement.PlayerState.Falling:
                    break;
                default:
                    break;
            }
            Move(playerMovement.walkingSpeed, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    private void Move(float speed, float inputX, float inputZ)
    {
        Vector2 speed2 = new Vector2(speed * inputX, speed * inputZ);
        //speed2.Normalize();
        rigidbody.velocity = new Vector3(speed2.x, rigidbody.velocity.y, speed2.y);
    }

    public void Jump()
    {
        playerMovement.playerState = PlayerMovement.PlayerState.Jumping;
        rigidbody.AddForce(new Vector2(0, playerMovement.jumpForce), ForceMode.Impulse);

    }
}
