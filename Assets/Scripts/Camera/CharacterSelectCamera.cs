using UnityEngine;
using System.Collections;

public class CharacterSelectCamera : MonoBehaviour {

    [SerializeField]private Transform[] mounts;
    private CURRENT_SHIP currentShip;
    private Transform mount;
    private float currentLerpTime;
    private float lerpTime;

    enum CURRENT_SHIP
    {
        FIRST,
        SECOND,
        THIRD,
        FOURTH
    }

    void Start()
    {
        currentShip = CURRENT_SHIP.FIRST;
        mount = mounts[0];

        currentLerpTime = 0.0f;
        lerpTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentLerpTime += Time.smoothDeltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float percent = currentLerpTime / lerpTime;
        percent = percent * percent * percent * (percent * (percent * 6f - 15f) + 10f);

        transform.position = Vector3.Lerp(transform.position, mount.position, percent / 6);
        transform.rotation = Quaternion.Slerp(transform.rotation, mount.rotation, percent / 6);

        switch (currentShip)
        {
            case CURRENT_SHIP.FIRST:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SetMount(mounts[1], CURRENT_SHIP.SECOND);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SetMount(mounts[3], CURRENT_SHIP.FOURTH);
                }
                break;
            case CURRENT_SHIP.SECOND:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SetMount(mounts[2], CURRENT_SHIP.THIRD);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SetMount(mounts[0], CURRENT_SHIP.FIRST);
                }
                break;
            case CURRENT_SHIP.THIRD:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SetMount(mounts[3], CURRENT_SHIP.FOURTH);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SetMount(mounts[1], CURRENT_SHIP.SECOND);
                }
                break;
            case CURRENT_SHIP.FOURTH:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SetMount(mounts[0], CURRENT_SHIP.FIRST);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    SetMount(mounts[2], CURRENT_SHIP.THIRD);
                }
                break;
        }
    }

    void SetMount(Transform newMount, CURRENT_SHIP ship)
    {
        mount = newMount;
        currentShip = ship;
    }

}
