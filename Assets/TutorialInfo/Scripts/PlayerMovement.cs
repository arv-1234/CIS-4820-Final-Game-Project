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

    Vector3 playerVelocity;
    Vector3 rotateDirection;
    float yVelocity = 0.0f;
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animation>();  // Use Legacy Animation
    }

    void Update()
    {
        // **Capture movement input**
        Vector3 inputDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        inputDirection = transform.TransformDirection(inputDirection); // Convert local to world space

        // **Handle running**
        if (Input.GetKey(KeyCode.P))
        {
            playerAnimator.CrossFade("Run", 0.1f);
            inputDirection *= runSpeed;
        }
        else
        {
            if (inputDirection.magnitude == 0 && !isJumping)  
            {
                playerAnimator.CrossFade("Idle", 0.1f);
            }
            else if (!isJumping)  
            {
                playerAnimator.CrossFade("Walk", 0.1f);
                inputDirection *= moveSpeed;
            }
        }

        // **Maintain forward momentum while airborne**
        if (IsGrounded()) 
        {
            yVelocity = 0; // Reset vertical velocity when grounded

            if (isJumping)
            {
                playerAnimator.CrossFade("Idle", 0.1f);  
            }
            isJumping = false;

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump");
                playerAnimator.CrossFade("Jump", 0.1f);
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumping = true;
            }

            // Apply new movement input only when grounded
            playerVelocity = inputDirection;
        }
        else
        {
            // Apply gravity while in the air
            yVelocity += gravity * Time.deltaTime;

            // Keep momentum while airborne (preserve x and z)
            playerVelocity.x = inputDirection.x;
            playerVelocity.z = inputDirection.z;
        }

        // **Apply vertical movement**
        playerVelocity.y = yVelocity;

        // **Handle rotation**
        float moveHorz = Input.GetAxis("Horizontal");
        if (moveHorz > 0)
            rotateDirection = new Vector3(0, 1, 0);
        else if (moveHorz < 0)
            rotateDirection = new Vector3(0, -1, 0);
        else
            rotateDirection = Vector3.zero;

        controller.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);

        // **Move the character**
        controller.Move(playerVelocity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);
    }
}
