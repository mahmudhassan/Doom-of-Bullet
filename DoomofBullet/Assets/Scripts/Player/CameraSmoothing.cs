//Smooth Camera movement from Unity Stealth
using UnityEngine;
using System.Collections;

public class CameraSmoothing : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    public Quaternion startingRot;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        startingRot = gameObject.transform.rotation;
        
    }

    void Update()
    {
        //transform.eulerAngles.z = startingRot.eulerAngles.z;
        var rot = startingRot.eulerAngles.z;
        

    }
}

/*
     public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    } 
 */
