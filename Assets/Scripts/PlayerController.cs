using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string horizontalKeycode = "Horizontal";
    private const string verticalKeycode = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private bool breakInput = false;
    private float currentSteerAngle;
    private float currentBreakingForce;

    private Rigidbody carRb;

    [SerializeField] private float motorForce;
    [SerializeField] private float BreakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    private void Start()
    {
        carRb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheel();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(horizontalKeycode);
        verticalInput = Input.GetAxis(verticalKeycode);
        breakInput = Input.GetKey(KeyCode.Space);
    }
    private void HandleMotor()
    {
        //Handles breaking
        Vector3 velocity = carRb.velocity;
        Vector3 localVel = transform.InverseTransformDirection(velocity);

        if (verticalInput < 0 && localVel.z > 0.5)
        {
            currentBreakingForce = BreakForce/2;
        }
        else if (verticalInput > 0 && localVel.z < -0.5)
        {
            currentBreakingForce = BreakForce/2;
        }
        else if(breakInput)
        {
            currentBreakingForce = BreakForce;
        }
        else
        {
            currentBreakingForce = 0;
        }

        frontLeftWheelCollider.motorTorque = verticalInput * motorForce * (currentBreakingForce > 0 ? 0 : 1);
        frontRightWheelCollider.motorTorque = verticalInput * motorForce * (currentBreakingForce > 0 ? 0 : 1);


        ApplyBreaks();
    }
    private void ApplyBreaks()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakingForce;
        frontRightWheelCollider.brakeTorque = currentBreakingForce;
        backLeftWheelCollider.brakeTorque = currentBreakingForce;
        backRightWheelCollider.brakeTorque = currentBreakingForce;
    }
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    private void UpdateWheel()
    {
        updateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        updateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        updateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        updateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    private void updateSingleWheel(WheelCollider _wheelCollider, Transform _wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        _wheelCollider.GetWorldPose(out pos, out rot);
        _wheelTransform.position = pos;
        _wheelTransform.rotation = rot;
    }


}
