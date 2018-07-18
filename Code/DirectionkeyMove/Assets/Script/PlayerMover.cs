using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {

    private CharacterController controller;
    private Vector3 moveVector;

    public float speed;
    private float verticalVelocity;
    private float gravity = 12.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveVector.x = Input.GetAxis("Horizontal") * speed;

        moveVector.y = verticalVelocity;

        moveVector.z = Input.GetAxis("Vertical") * speed;

        controller.Move(moveVector * Time.deltaTime);
        //float moveHorizontal = Input.GetAxis("Horizontal"); //Horizontal
        //float moveVertical = Input.GetAxis("Vertical"); //Vertical
    }
}