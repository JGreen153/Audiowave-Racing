using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]private float accel;
    [SerializeField]private float turnSpeed;
    [SerializeField]private Transform visualRotation;

    private Rigidbody rb;

    private float brakeStrength;

    private float initialAcceleration;
    private float velocityRef;

    private bool boost;
    public bool IsBoosting { get { return boost; } }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1000, 0);
        rb.maxAngularVelocity = 2;

        brakeStrength = 0;

        boost = false;

        initialAcceleration = accel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CountdownManager.canDrive)
            Movement();
    }

    void Movement()
    {
        Vector3 forwardInput = new Vector3(0, 0, Input.GetAxis("Vertical"));

        rb.AddRelativeForce(forwardInput * accel * rb.mass);
        rb.AddRelativeTorque(Vector3.up * turnSpeed * Input.GetAxis("Horizontal") * rb.mass);

        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, Input.GetAxis("Horizontal") * -75, ref velocityRef, .25f);
        visualRotation.transform.eulerAngles = newRotation;

        rb.AddForce(-Vector3.Project(rb.velocity, transform.right) * (rb.mass * 3), ForceMode.Force);

        Brake();
    } 

    void Brake()
    {
        brakeStrength = Mathf.Clamp(brakeStrength, 0, 1);

        if (Input.GetKey(KeyCode.S))
        {
            brakeStrength += 0.02f;
        }
        else if (brakeStrength > 0)
        {
            brakeStrength -= 0.1f;
        }

        rb.drag = brakeStrength;          
    }

    public void ActivateBoost(float speedmultiplier, float duration)
    {
        if (duration > 0 && accel < initialAcceleration * speedmultiplier)
        {
            accel *= speedmultiplier;
            boost = true;
        }
        else if(duration <= 0)
        {
            accel = initialAcceleration;
            boost = false;
        }
    }

}
