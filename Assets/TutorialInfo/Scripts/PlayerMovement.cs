using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animation playerAnimator;

    private float gravity = -9.81f;
    public float moveSpeed = 5.0f;
    public float runSpeed = 8.0f;
    public float jumpHeight = 5.0f;
    public float rotateSpeed = 50.0f;
    public float airSpeedMultiplier = 1.5f; // Added air speed multiplier

    Vector3 playerVelocity;
    Vector3 rotateDirection;
    float yVelocity = 0.0f;
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animation>();
    }

    void Update()
    {
        // Get input for forwrd/backward movement 
        Vector3 moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

        // Convert from local to world space direction
        moveDirection = transform.TransformDirection(moveDirection);

        // Determine movement speed (walk or run)
        float currentSpeed; // made change here

        if(Input.GetKey(KeyCode.P))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        // Apply air speed multiplier if player is not grounded
        if(!IsGrounded())
        {
            currentSpeed *= airSpeedMultiplier;
        }

        // apply calculated speed to movement direction
        moveDirection *= currentSpeed;

        // Handle animations when on the ground 
        if(IsGrounded())
        {
            if(Input.GetKey(KeyCode.P)) // Running 
            {
                playerAnimator.CrossFade("Run", 0.1f);
            }
            else // Walking or Idle
            {
                if(moveDirection.magnitude == 0)
                {
                    playerAnimator.CrossFade("Idle", 0.1f);
                }
                else
                {
                    playerAnimator.CrossFade("Walk", 0.1f);
                }
            }
        }

        // Handle gravity and jumoing 
        if(IsGrounded())
        {
            yVelocity = 0; // Reset vertical velocity when on the ground 

            // Return to idle after landing 
            if(isJumping)
            {
                playerAnimator.CrossFade("Idle", 0.1f);
            }
            isJumping = false;

            // Jump if button is pressed 
            if(Input.GetButton("Jump"))
            {
                //Debug.Log("Jump");
                playerAnimator.CrossFade("Jump", 0.1f);
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumping = true;
            }

            // Apply movement when grounded 
            playerVelocity = moveDirection;
        }
        else // In mid-air
        {
            
            yVelocity += gravity * Time.deltaTime;

            // Maintain horizontal movement while in the air 
            playerVelocity.x = moveDirection.x;
            playerVelocity.y = moveDirection.y;
        }

        // Apply vertical velocity 
        playerVelocity.y = yVelocity;

        float moveHorz = Input.GetAxis("Horizontal");

        if(moveHorz > 0) // Rotate right
        {
            rotateDirection = new Vector3(0, 1, 0);
        }
        else if (moveHorz < 0) // Rotate left 
        {
            rotateDirection = new Vector3(0, -1, 0);
        }
        else // Rotation
        {
            rotateDirection = Vector3.zero;
        }

        controller.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    // Check if player is toching the ground
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);
    }
}