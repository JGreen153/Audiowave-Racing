using UnityEngine;
using System.Collections;

public class TestMovement : MonoBehaviour {

    private Rigidbody rb;

    private float velocityRef;

    [SerializeField]
    private float accel;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private Transform visualRotation;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1000, 0);
        rb.maxAngularVelocity = 2;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Movement();
	}

    void Movement()
    {
        Vector3 speed = Vector3.forward * accel * Input.GetAxis("Vertical") * rb.mass;
        Vector3 torque = Vector3.up * turnSpeed * Input.GetAxis("Horizontal") * rb.mass;

        rb.AddRelativeForce(speed);
        rb.AddRelativeTorque(torque);

        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, Input.GetAxis("Horizontal") * -75.0f, ref velocityRef, .25f);
        visualRotation.transform.eulerAngles = newRotation;

        rb.AddForce(-Vector3.Project(rb.velocity, transform.right) * (rb.mass * 3), ForceMode.Force);
    }
}
