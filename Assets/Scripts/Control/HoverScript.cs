using UnityEngine;
using System.Collections;

public class HoverScript : MonoBehaviour {

    public delegate void Respawning();
    public event Respawning OnRespawn;

    private Rigidbody rb;
    [SerializeField]private Transform hoverPoint;

    public static float hoverHeight = 3.0f;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Hover();
    }

    void Hover()
    {
        RaycastHit hit;
        int mask = 1 << 8;

        if (Physics.Raycast(hoverPoint.position, Vector3.down, out hit, hoverHeight, mask))
        {
            Vector3 height = hit.point;
            rb.position = new Vector3(rb.position.x, height.y + (hoverHeight - 1), rb.position.z);
            rb.AddRelativeForce(Vector3.up * 9.81f * rb.mass);
        }
        else
        {
            if (OnRespawn != null)
                OnRespawn();
        }
    }

}
