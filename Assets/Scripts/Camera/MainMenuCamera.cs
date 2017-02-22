using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour {

    [SerializeField]private Transform mount;
    private float currentLerpTime;
    private float lerpTime;

	// Use this for initialization
	void Start ()
    {
        currentLerpTime = 0.0f;
        lerpTime = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

        currentLerpTime += Time.smoothDeltaTime;
        if(currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float percent = currentLerpTime / lerpTime;
        percent = percent * percent * percent * (percent * (percent * 6f - 15f) + 10f);

        transform.position = Vector3.Lerp(transform.position, mount.position, percent / 6);
        transform.rotation = Quaternion.Slerp(transform.rotation, mount.rotation, percent / 6);	
	}

    public bool GetMount(Transform currentMount)
    {
        if (mount != currentMount)
            return false;

        return true;
    }

    public void SetMount(Transform newMount)
    {
        mount = newMount;
    }

}
