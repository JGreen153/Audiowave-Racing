using UnityEngine;
using System.Collections;

public class GuideManager : MonoBehaviour {

    private PositioningScript player;

    private Vector3 direction;
    private Quaternion waypoint;

    private float currentLerpTime;
    private float lerpTime = 1;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<PositioningScript>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLerpTime += Time.smoothDeltaTime;
        if (currentLerpTime > lerpTime)
            currentLerpTime = lerpTime;

        float percent = currentLerpTime / lerpTime;
        percent = percent * percent * percent * (percent * (percent * 6f - 15f) + 10f);

        transform.position = player.transform.position + new Vector3(0, .5f, 0);

        direction = (player.TargetWaypoint.position - player.transform.position);
        direction.Normalize();

        waypoint = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, waypoint, percent / 10);
    }

}
