using UnityEngine;
using System.Collections;

public class AudioDeformShader : MonoBehaviour {

    private float lerpTime = 0.0f;

    private ReadAudioData data;

    private Material material;

    private Color colour;
    private Color newColour;

    // Use this for initialization
    void Start()
    {
        data = FindObjectOfType<ReadAudioData>();
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ShaderVariables();
    }

    void ShaderVariables()
    {
        lerpTime += Time.smoothDeltaTime;

        float percent = lerpTime / 3.0f;
        percent = percent * percent * percent * (percent * (6.0f * percent - 15.0f) + 10.0f);

        if (lerpTime >= 3)
        {
            newColour = new Color(Random.Range(0, 0.5f), Random.Range(0, 0.5f), Random.Range(0, 0.5f));
            lerpTime = 0.0f;
        }
        else
        {
            colour = Color.Lerp(colour, newColour, percent);
        }

        material.SetFloat("_SubBass", data.SubBass);
        material.SetFloat("_Bass", data.Bass);
        material.SetFloat("_LowMidrange", data.LowerMidRange);
        material.SetFloat("_MidHighrange", data.MidRange + data.UpperMidRange);
        material.SetFloat("_PresenceAndBrilliance", data.Presence + data.Brilliance);

        material.SetColor("_RandomColour", colour);
    }
}
