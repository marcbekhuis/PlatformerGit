﻿using System.Collections;
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
                case PlayerMovement.PlayerState.Jumping:
                    break;
                case PlayerMovement.PlayerState.Falling:
                    break;
                default:
                    break;
            }
            Move(playerMovement.walkingSpeed * Input.GetAxis("Horizontal"));
        }
    }

    private void Move(float speed)
    {
        rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y, 0);
    }

    public void Jump()
    {
        playerMovement.playerState = PlayerMovement.PlayerState.Jumping;
        rigidbody.AddForce(new Vector2(0, playerMovement.jumpForce), ForceMode.Impulse);

    }
}