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
    }
    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        
        //Handles breaking
        Vector3 velocity = carRb.velocity;
        Vector3 localVel = transform.InverseTransformDirection(velocity);

        if (verticalInput < 0 && localVel.z > 0.5)
        {
            currentBreakingForce = BreakForce;
        }
        else if (verticalInput > 0 && localVel.z < -0.5)
        {
            currentBreakingForce = BreakForce;
        }
        else if(verticalInput == 0)
        {
            currentBreakingForce = BreakForce;
        }
        else
        {
            currentBreakingForce = 0;
        }
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