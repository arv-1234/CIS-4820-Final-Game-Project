using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 50.0f;
    public float jumpHeight = 3.0f;
    public float gravity = -9.81f;

    public CharacterController controller;
    private Animation playerAnimation;

    public Vector3 playerVelocity;
    public Vector3 rotateDirection;

    public float yVelocity = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
