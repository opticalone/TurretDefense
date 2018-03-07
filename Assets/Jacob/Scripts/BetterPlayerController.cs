using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerController : MonoBehaviour {

    private CharacterController controller;

    private float verticalVelocity;
    private float gravity = 14.0f;
    public float jumpForce = 10.0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }

            Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
            moveVector.x = Input.GetAxis("Horizontal") * 5.0f;
            moveVector.y = verticalVelocity;
            moveVector.z = Input.GetAxis("Vertical") * 5.0f;
            controller.Move(moveVector * Time.deltaTime);
        }
    }
}
