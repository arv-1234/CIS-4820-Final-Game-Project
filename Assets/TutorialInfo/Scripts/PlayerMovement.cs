using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private CharacterController controller;
    private Animation playerAnimator;  // Use Animator instead of Animation for more control
    private Vector3 startingPosition;  // Store starting position

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animation>();  // Use Animator for transitions
        startingPosition = transform.position;  // Record the starting position when the game starts
    }

    void Update()
    {

        // Get movement input
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = transform.forward * moveZ * moveSpeed;

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);

        // Play walking or idle animation based on movement
        if (moveZ != 0)
        {
            playerAnimator.CrossFade("Walk", 0.3f);
        }
        else
        {
            playerAnimator.CrossFade("Idle", 0.1f);
        }
    }
}
