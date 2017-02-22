using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PositioningScript>().TargetWaypoint == transform)
        {
            other.GetComponent<PositioningScript>().NextWaypoint();
        }
    }
}
