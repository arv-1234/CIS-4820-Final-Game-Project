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
    public float jumpHeight = 1.0f;
    public float rotateSpeed = 50.0f;

    Vector3 playerVelocity;
    Vector3 rotateDirection;
    public float yVelocity = 0;
    private bool isJumping = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animation>();  // Use Legacy Animation
    }

    void Update()
    {
        playerVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.P))
        {
            playerAnimator.CrossFade("Run", 0.1f);  // Play run animation if P is pressed
            playerVelocity *= runSpeed;
        }
        else
        {
            if (playerVelocity.magnitude == 0 && !isJumping)  // Prevent Idle when jumping
            {
                playerAnimator.CrossFade("Idle", 0.1f);  // Play idle if no movement
            }
            else if (!isJumping)  // Only transition to walk if not jumping
            {
                playerAnimator.CrossFade("Walk", 0.1f);  // Play walking animation
                playerVelocity *= moveSpeed;
            }
        }

        playerVelocity = transform.TransformDirection(playerVelocity);

        // **Jump Logic**
        if (controller.isGrounded)
        {
            if (isJumping)
            {
                playerAnimator.CrossFade("Idle", 0.1f);  // Once landed, transition back to Idle (Replace with play if need be)
            }
            isJumping = false;

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump");
                playerAnimator.CrossFade("Jump", 0.1f);  // Play Jump animation using Play()
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumping = true;
            }
        }

        yVelocity += gravity * Time.deltaTime;
        playerVelocity.y = yVelocity;

        float moveHorz = Input.GetAxis("Horizontal");
        if (moveHorz > 0)
            rotateDirection = new Vector3(0, 1, 0);
        else if (moveHorz < 0)
            rotateDirection = new Vector3(0, -1, 0);
        else
            rotateDirection = Vector3.zero;

        controller.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
