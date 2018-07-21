using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Mobile Touch Input Components
    public FixedJoystick MoveJoystick;
    public FixedButton JumpButton;
    //public FixedButton CrouchButton;

    //Vars & Properties
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController controller;

    // Use this for initialization
    void Start () {
        //Get The Controller From The Player GameObject
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {

        //Check for ground to prevent jump abuse
        if (controller.isGrounded)
        {
            //Grab input from MoveJoystick reference
            moveDirection = new Vector3(MoveJoystick.inputVector.x, 0, MoveJoystick.inputVector.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //Jumping Logic (grab input from jump button reference)
            if (JumpButton.Pressed)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        //Apply movements and simulate gravity
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
