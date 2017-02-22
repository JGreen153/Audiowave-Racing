using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoostManager : MonoBehaviour
{
    [SerializeField]private float speedMultiplier;
    [SerializeField]private float duration;

    private PlayerMovement player;

    private bool isBoosting;

    private bool fullBoost;
    public bool BoostIsFull { get { return fullBoost; } }

    private float timer;
    public float Timer { get { return timer; } }

    void Start()
    {
        player = GetComponent<PlayerMovement>();

        isBoosting = false;
        fullBoost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 1.9f)
        {
            fullBoost = true;
        }

        if (!fullBoost)
        {
            timer += 1 * Time.smoothDeltaTime;
        }

        if (fullBoost)
        {
            timer = 0.0f;
        }

        if(timer >= 10.0f)
        {
            duration = 2.0f;
        }

        if (Input.GetButton("Jump") && duration > 1.9f)
        {
            isBoosting = true;
        }

        if (isBoosting)
        {
            duration -= Time.smoothDeltaTime;
            player.ActivateBoost(speedMultiplier, duration);
        }

        if (duration <= 0.0f)
        {
            isBoosting = false;
            fullBoost = false;
        }
    }
}
