using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    //This script is placed on the players camera

    //Grab Rotation Screen Field
    public FixedTouchField RotationField;
    
    //Vars
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 3.0f;
    public float smoothing = 1.0f;
    public Transform aimLoc;
    public AimIK aimIk;
    GameObject character;


    // Use this for initialization
    void Start () {
        //Get character to rotate left to right only
        character = this.transform.parent.gameObject;

        // [KEEP DISABLED FOR PC TESTING & ENABLED FOR MOBILE/TOUCH]
        // (On start hide and lock cursor position)
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update () {
        //Grabs from the FixedTouchField instead of mouse x / y for mobile controll
        var md = new Vector2(RotationField.TouchDist.x, RotationField.TouchDist.y);

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        //Locks ability to look past stright up and down
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up);
    }

    void LateUpdate()
    {
        //Set the aimer pos to the aimLoc pos connected to camera
        aimIk.solver.IKPosition = aimLoc.position;
    }
}
