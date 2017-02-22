using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    private PlayerMovement player;

    [SerializeField]
    private bool isShaking;

    [SerializeField]
    private float scale;
    [SerializeField]
    private float speed;

    private float force;

    private Vector3 initialRotation;

    private Quaternion newRotation;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        isShaking = false;

        initialRotation = transform.rotation.eulerAngles;

        newRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        force = Mathf.Sin(Time.time * speed) * scale;

        if(player.IsBoosting)
        {
            isShaking = true;
        }
        else if(!player.IsBoosting)
        {
            isShaking = false;
        }

        if (isShaking)
        {
            newRotation.x = Mathf.Lerp(newRotation.x, force, 1.0f / Time.smoothDeltaTime);
            transform.rotation = newRotation;
        }
        else
        {
            newRotation.eulerAngles = Vector3.Slerp(transform.rotation.eulerAngles, initialRotation, 1.0f / Time.smoothDeltaTime);
            transform.rotation = newRotation;
        }

        transform.position = Camera.main.transform.position;

    }
}
