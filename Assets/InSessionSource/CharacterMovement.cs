using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour, InputListener
{
    [SerializeField] public float moveSpeed =0f;
    [SerializeField] private float acceleration = 5;
    [SerializeField] private float deceleration = 5;
    [SerializeField] private float angularDampening = 90;
    [SerializeField] private float jumpSpeed = 90;

    [SerializeField] private Vector3 airborenCheckStart;
    [SerializeField] private Vector3 airborenCheckDirection;
    [SerializeField] private float airborneCheckDistance;
    [SerializeField] LayerMask airborneCheckMask;
    [SerializeField] private float lowJumpGravity;


    private Vector2 inputVector;
    private Vector2 wetInputVector;
    private Vector3 lastForward;
    private Rigidbody rigidBody;
    private float jumpValue;
    private bool Airborne
    {
        get
        {
            Ray r = new Ray(transform.TransformPoint(airborenCheckStart), transform.TransformDirection(airborenCheckDirection));
            return !Physics.Raycast(r, airborneCheckDistance, airborneCheckMask);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (context.action.name != "Motion") return;
        inputVector = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.action.name != "Jump") return;
        jumpValue = context.ReadValue<float>();
        if (!Airborne) rigidBody.AddForce(Vector3.up * jumpSpeed * jumpValue, ForceMode.VelocityChange);
    }

    void DampenInput()
    {
        float x = wetInputVector.x;
        float y = wetInputVector.y;
        ;
        if (inputVector.x == 0)
        {
            x = Mathf.MoveTowards(x, 0f, Time.deltaTime * deceleration);
        }
        else
        {
            x = Mathf.MoveTowards(x, inputVector.x, Time.deltaTime * acceleration);

        }
        if (inputVector.y == 0)
        {
            y = Mathf.MoveTowards(y, 0f, Time.deltaTime * deceleration);

        }
        else
        {
            y = Mathf.MoveTowards(y, inputVector.y, Time.deltaTime * acceleration);

        }
        wetInputVector.Set(x, y);
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        DampenInput();

        if(rigidBody.velocity.y < 0)
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime;
        }
        else if (rigidBody.velocity.y > 0 && jumpValue <=0.1f)
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y *lowJumpGravity* Time.fixedDeltaTime;

        }
        Transform cameraTransform = Camera.main.transform;
        Vector3 rigth = Vector3.ProjectOnPlane(cameraTransform.right, transform.up);
        Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, transform.up);
        Vector3 motionVector = rigth * (wetInputVector.x * moveSpeed * Time.fixedDeltaTime) + forward * (wetInputVector.y * moveSpeed * Time.fixedDeltaTime);
        transform.Translate(motionVector, Space.World);
        if (motionVector.magnitude > 0)
        {
            transform.forward = Vector3.Slerp(lastForward.normalized, motionVector.normalized, Time.deltaTime * angularDampening);
        }
        lastForward = transform.forward;
    }

    public Action<InputAction.CallbackContext>[] ListenerFunction => new Action<InputAction.CallbackContext>[] { Move, Jump };

    private void OnDrawGizmos()
    {
        Gizmos.color = Airborne ? Color.red : Color.green;
        Vector3 rayStart = transform.TransformPoint(airborenCheckStart);
        Gizmos.DrawLine(rayStart, rayStart + transform.TransformDirection(airborenCheckDirection) * airborneCheckDistance);
    }

}
