using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 100f;
    [SerializeField] private float rotationThrust = 1f;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem leftThrusterParicles;
    [SerializeField] private ParticleSystem rightThrusterParicles;

    private Rigidbody playerRB;
    private AudioSource audioSource;
    private bool isTransitioning;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightThrusterParicles.isPlaying)
            {
                rightThrusterParicles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
            if (!leftThrusterParicles.isPlaying)
            {
                leftThrusterParicles.Play();
            }
        }
        else
        {
            rightThrusterParicles.Stop();
            leftThrusterParicles.Stop();
        }
    }

    private void ApplyRotation(float rotationThrustThisFrame)
    {
        playerRB.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThrustThisFrame * Time.deltaTime);
        playerRB.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}