using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {

    private Transform target;
    [SerializeField]private float autoRotationSpeed = 1.0f;
    [SerializeField]private float distance = 3.0f;

    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }

    void Start()
    {
        target = GetComponent<CameraControl>().Player;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(Vector3.up, autoRotationSpeed);

        transform.position = target.position;
        transform.position -= Vector3.forward * distance;
        transform.LookAt(target);
    }
}
