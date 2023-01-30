using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 100f;
    [SerializeField] private float rotationThrust = 1f;

    private Rigidbody playerRB;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            playerRB.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThrustThisFrame)
    {
        playerRB.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThrustThisFrame * Time.deltaTime);
        playerRB.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}