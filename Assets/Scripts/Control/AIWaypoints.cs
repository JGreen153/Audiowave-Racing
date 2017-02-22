using UnityEngine;

public class AIWaypoints : MonoBehaviour {
	
	[SerializeField]private Transform[] track;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < track.Length; i++)
        {
            if (i > 0)
            {
                Gizmos.DrawLine(track[i - 1].transform.position, track[i].transform.position);
            }
        }
    }

    public Transform Next(Transform currentWaypoint)
    {
        int j = 0;

        for (int i = 0; i < track.Length; i++)
        {
            if (track[i] == currentWaypoint && j < track.Length - 1)
            {
                return track[i + 1];
                
            } else if (j == track.Length - 1)
            {
                j = 0;
                return track[0];
            }

            j++;
        
        }

        return track[0];
    }

    public Transform Previous(Transform currentWaypoint)
    {
        Transform prev = track[track.Length - 1];

        for(int i = 0; i < track.Length; i++)
        {
            if (i > 0)
            {
                if (currentWaypoint == track[i])
                {
                    prev = track[i - 1];
                }
            }
        }

        return prev;
    }

    public int GetTrackLength()
    {
		return track.Length;
	}

	public Transform FirstWaypoint()
    {
		return track[0];
	}

}

	