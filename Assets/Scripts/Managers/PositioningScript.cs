using UnityEngine;
using System.Collections;
using System;

public class PositioningScript : MonoBehaviour, IComparable<PositioningScript>
{
    public delegate void UpdateLapText();
    public static event UpdateLapText OnUpdateLapText;

    private Rigidbody rb;

    private AIWaypoints waypointScript;
    private HoverScript hoverScript;
    private Transform targetWaypoint;

    private float progress;
    private int checkpointNo;
    private int lapNo;

    public int LapNo { get { return lapNo; } }
    public Transform TargetWaypoint { get { return targetWaypoint; } }

    void Awake()
    {
        hoverScript = GetComponent<HoverScript>();
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        waypointScript = FindObjectOfType<AIWaypoints>();
        targetWaypoint = waypointScript.FirstWaypoint();

        checkpointNo = 0;
        lapNo = 0;
    }

    void OnEnable()
    {
        hoverScript.OnRespawn += Respawn;
    }

    // Update is called once per frame
    void Update()
    {
        float percent = (100 / (targetWaypoint.position - rb.position).magnitude);
        progress = ((lapNo + 1) * 10000) + (checkpointNo * 100) + percent;

        if (gameObject.CompareTag("Player"))
        {
            Position();
        }
        else
        {
            AIPosition();
        }
    }

    void AIPosition()
    {
        if(checkpointNo >= waypointScript.GetTrackLength() && lapNo < 3)
        {
            lapNo++;
            checkpointNo = 0;
        }
    }

    void Position()
    {
        if (checkpointNo >= waypointScript.GetTrackLength() && LapManager.currentLap < 3)
        {
            LapManager.currentLap++;
            lapNo++;
            checkpointNo = 0;

            if (OnUpdateLapText != null)
                OnUpdateLapText();
        }
        else if (LapManager.currentLap == 3 && checkpointNo >= waypointScript.GetTrackLength())
        {
            lapNo++;
            checkpointNo = 0;
        }
    }

    void OnDisable()
    {
        hoverScript.OnRespawn -= Respawn;
    }

    public void NextWaypoint()
    {
        targetWaypoint = waypointScript.Next(targetWaypoint);
        checkpointNo++;
    }

    public int CompareTo(PositioningScript other)
    {
        if (progress > other.progress)
        {
            return -1;
        }
        else if (progress < other.progress)
        {
            return 1;
        }

        return (int)progress;
    }

    void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.position = waypointScript.Previous(targetWaypoint).position;
    }

}
