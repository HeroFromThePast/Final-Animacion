using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 camOffset;

    [Range(0.1f, 1.0f)]
    public float SmoothFactor = 0.1f;

    public bool rotacionActive;
    public float velRotacionn = 5.0f;

    public bool lookAtPlayer = false;
    void Start()
    {
        camOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotacionActive)
        {
            Quaternion camTurnAngle = 
                Quaternion.AngleAxis(Input.GetAxis("Mouse X")* velRotacionn, Vector3.up);
            camOffset = camTurnAngle * camOffset;
        }

        Vector3 newPos = player.position + camOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if(lookAtPlayer || rotacionActive)
        {
            transform.LookAt(player);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            rotacionActive = true;
        }
        else
        {
            rotacionActive = false;
            transform.LookAt(player);
        }
    }
}
