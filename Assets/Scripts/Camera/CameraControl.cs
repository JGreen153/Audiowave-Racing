using UnityEngine;

public class CameraControl : MonoBehaviour {

	private GameObject player;
	[SerializeField]private float distance;
	[SerializeField]private float height;
    private float currentLerpTime;
    private float lerpTime;

    public Transform Player { get { return player.transform; } }

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentLerpTime = 0.0f;
        lerpTime = 1.0f;
    }

    void LateUpdate()
    {
        currentLerpTime += Time.smoothDeltaTime;
        if(currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float percent = currentLerpTime / lerpTime;
        percent = percent * percent * percent * (percent * (percent * 6f - 15f) + 10f);

        float currentRotationAngle = transform.eulerAngles.y;
        float wantedRotationAngle = player.transform.eulerAngles.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, percent / 10);

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = player.transform.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.position = transform.position + new Vector3(0, height, 0);
        transform.LookAt(player.transform);
    }
}
