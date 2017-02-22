using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private Transform visualRotation;

    public static float accel;  
    public static float turnSpeed;

    private AIWaypoints waypointScript;
    private PositioningScript positionScript;

    private Transform targetWaypoint;

    public static float speedLimit;
    public static float distance;

    // Use this for initialization
    void Start()
    {
        waypointScript = FindObjectOfType<AIWaypoints>();
        positionScript = GetComponent<PositioningScript>();

        targetWaypoint = waypointScript.FirstWaypoint();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CountdownManager.canDrive)
            Movement();
    }

    void Movement()
    {
        targetWaypoint = positionScript.TargetWaypoint;

        Vector3 localNextTargetPos = transform.InverseTransformPoint(waypointScript.Next(targetWaypoint).position);

        Vector3 distanceFromTarget = targetWaypoint.position - transform.position;
        
        rb.AddRelativeForce(Vector3.forward * accel * rb.mass);

        float turnDotProd = Vector3.Dot(transform.right, distanceFromTarget) * Time.smoothDeltaTime;

        if (turnDotProd > 0.1f)
        {
            rb.AddRelativeTorque(Vector3.up * turnSpeed * rb.mass);
        }
        else if (turnDotProd < -0.1f)
        {
            rb.AddRelativeTorque(Vector3.down * turnSpeed * rb.mass);
        }

        float currentSpeed = rb.velocity.magnitude * Mathf.PI;
        float targetAngle = Vector3.Angle(transform.forward, localNextTargetPos);

        if (distanceFromTarget.sqrMagnitude < distance * distance && currentSpeed > speedLimit && targetAngle > 65)
        {
            rb.drag = 2.5f;
        }
        else
        {
            rb.drag = 0.0f;
        }

        if (visualRotation.localEulerAngles.z > 0.5f || visualRotation.localEulerAngles.z < -0.5f)
            visualRotation.localEulerAngles = new Vector3(0, visualRotation.localEulerAngles.y, 0);

        rb.AddForce(-Vector3.Project(rb.velocity, transform.right) * (rb.mass * 3), ForceMode.Force);
    }

}
