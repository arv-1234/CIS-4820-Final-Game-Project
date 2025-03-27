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
        Vector3 moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

        moveDirection = transform.TransformDirection(moveDirection);

        float currentSpeed; // made change here

        if(Input.GetKey(KeyCode.P))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        if(!IsGrounded())
        {
            currentSpeed *= airSpeedMultiplier;
        }

        moveDirection *= currentSpeed;

        if(IsGrounded())
        {
            if(Input.GetKey(KeyCode.P))
            {
                playerAnimator.CrossFade("Run", 0.1f);
            }
            else
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

        if(IsGrounded())
        {
            yVelocity = 0;

            if(isJumping)
            {
                playerAnimator.CrossFade("Idle", 0.1f);
            }
            isJumping = false;

            if(Input.GetButton("Jump"))
            {
                //Debug.Log("Jump");
                playerAnimator.CrossFade("Jump", 0.1f);
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumping = true;
            }
            playerVelocity = moveDirection;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
            playerVelocity.x = moveDirection.x;
            playerVelocity.y = moveDirection.y;
        }

        playerVelocity.y = yVelocity;

        float moveHorz = Input.GetAxis("Horizontal");

        if(moveHorz > 0)
        {
            rotateDirection = new Vector3(0, 1, 0);
        }
        else if (moveHorz < 0)
        {
            rotateDirection = new Vector3(0, -1, 0);
        }
        else
        {
            rotateDirection = Vector3.zero;
        }

        controller.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);
    }
}