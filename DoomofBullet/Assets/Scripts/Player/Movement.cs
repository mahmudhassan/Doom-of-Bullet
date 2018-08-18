using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Mobile Touch Input Components
    public FixedJoystick MoveJoystick;
    public FixedButton JumpButton;
    public FixedButton CrouchButton;

    //Vars & Properties
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool isCrouching = false;
    CharacterController controller;
    Animator anim;

    // Use this for initialization
    void Start () {
        //Get The Controller From The Player GameObject
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
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

            //Controlls forward back
            anim.SetFloat("Speed", MoveJoystick.inputVector.y);
            anim.SetFloat("Side", MoveJoystick.inputVector.x);
        }

        //Apply movements and simulate gravity
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    //Methods called from GUI buttons
    public void onJump()
    {
        if (!isCrouching && controller.isGrounded)
        {
            anim.SetTrigger("JumpT");
            moveDirection.y = jumpSpeed;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    public void onCrouch()
    {
        //isCrouching = !isCrouching;
        if (!isCrouching)
        {
            isCrouching = true;
            anim.SetBool("Crouching", isCrouching);
        }
        else
        {
            isCrouching = false;
            anim.SetBool("Crouching", isCrouching);
        }
    }
}
