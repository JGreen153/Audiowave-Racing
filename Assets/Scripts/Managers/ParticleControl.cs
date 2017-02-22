using UnityEngine;
using System.Collections;

public class ParticleControl : MonoBehaviour {

    private ParticleSystem shipParticle;

    private Rigidbody rb;

    private PlayerMovement player;

    private float particleSize;

    private float fov;
    private float fovMax;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = player.GetComponent<Rigidbody>();

        shipParticle = player.GetComponentInChildren<ParticleSystem>();
        particleSize = shipParticle.startSize;

        fov = Camera.main.fieldOfView;
        fovMax = 120.0f;
	}

    // Update is called once per frame
    void Update ()
    {
        float speed = rb.velocity.magnitude * Mathf.PI;

        shipParticle.SetEmissionRate(speed * 0.5f);

        if (player.IsBoosting)
        {
            shipParticle.startSize += 0.5f * Time.smoothDeltaTime;
        }
        else if (!player.IsBoosting && shipParticle.startSize > particleSize)
        {
            shipParticle.startSize -= 0.5f * Time.smoothDeltaTime;
        }

        fov = Mathf.Clamp(fov, 60.0f, fovMax);

        if (player.IsBoosting && fovMax < 160.0f)
        {
            fovMax += 30 * Time.smoothDeltaTime;
        }
        else if (!player.IsBoosting && fovMax > 120.0f)
        {
            fovMax -= 30 * Time.smoothDeltaTime;
        }

        if (Input.GetAxis("Vertical") > 0 && CountdownManager.canDrive)
        {
            fov += (speed * 0.5f) * Time.smoothDeltaTime;
        }
        else
        {
            fov -= 30 * Time.smoothDeltaTime;
        }

        Camera.main.fieldOfView = fov;
    }

}

public static class ParticleSystemExtension
{
    public static void EnableEmission(this ParticleSystem particleSystem, bool enabled)
    {
        var emission = particleSystem.emission;
        emission.enabled = enabled;
    }

    public static float GetEmissionRate(this ParticleSystem particleSystem)
    {
        return particleSystem.emission.rate.constantMax;
    }

    public static void SetEmissionRate(this ParticleSystem particleSystem, float emissionRate)
    {
        var emission = particleSystem.emission;
        var rate = emission.rate;
        rate.constantMax = emissionRate;
        emission.rate = rate;
    }
}
