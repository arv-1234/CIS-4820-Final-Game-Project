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

    public float jumpHeight = 1.0f;

    public float rotateSpeed = 50.0f;

    Vector3 playerVelocity;

    Vector3 rotateDirection;

    public float yVelocity = 0;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animation>();  // Use Animator for transitions
    }
      

    void Update()
    {
        playerVelocity = new Vector3(0, 0, Input.GetAxis("Vertical"));

        if(playerVelocity.magnitude == 0)
        {
            playerAnimator.CrossFade("Idle", 0.1f);
        }
        else
        {
            playerAnimator.CrossFade("Walk", 0.1f);
        }
        
        playerVelocity = transform.TransformDirection(playerVelocity);

        playerVelocity *= moveSpeed;

        Debug.Log(controller.isGrounded);
        if(controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * (gravity));
            playerAnimator.CrossFade("Jump", 0.2f);
        }

        yVelocity += gravity * Time.deltaTime;

        playerVelocity.y = yVelocity;

        float moveHorz = Input.GetAxis("Horizontal");
        if (moveHorz > 0) //right turn - rotate clockwise, or about +Y
            rotateDirection = new Vector3(0, 1, 0);
        else if (moveHorz < 0) //left turn – rotate counter-clockwise, or about -Y
            rotateDirection = new Vector3(0, -1, 0);
        else
            rotateDirection = new Vector3(0, 0, 0);

        controller.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);
        CollisionFlags flags = controller.Move(playerVelocity * Time.deltaTime);

    }
}
