﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirdPersonMovement : MonoBehaviour
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

    public void Movement(float inputX, float inputZ)
    {
        this.transform.eulerAngles += new Vector3(0,Input.GetAxis("Mouse X") * playerMovement.mouseSensitivity,0);
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
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
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && playerMovement.stamina > 0)
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

        rigidbody.velocity = rigidbody.rotation * new Vector3(speed2.x, rigidbody.velocity.y, speed2.y);
    }

    public void Jump()
    {
        audioSource.PlayOneShot(jumpSound);
        playerMovement.playerState = PlayerMovement.PlayerState.Jumping;
        rigidbody.AddForce(new Vector2(0, playerMovement.jumpForce), ForceMode.Impulse);

    }
}
