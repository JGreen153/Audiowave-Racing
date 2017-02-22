using UnityEngine;

public class ReadAudioData : MonoBehaviour
{
    private AudioSource aSource;

    private float subBass, bass, lowerMidrange, midRange, upperMidrange, presence, brilliance = 0.0f;

    public float SubBass { get { return subBass; } }
    public float Bass { get { return bass; } }
    public float LowerMidRange { get { return lowerMidrange; } }
    public float MidRange { get { return midRange; } }
    public float UpperMidRange { get { return upperMidrange; } }
    public float Presence { get { return presence; } }
    public float Brilliance { get { return brilliance; } }

    private float[] spectrum = new float[1024];

    /* spectrum[0] = 0hz,
       spectrum[1] = 23.4hz,
       spectrum[2] = 46.8hz */

    // Use this for initialization
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetFrequencies();
    }

    void GetFrequencies()
    {
        aSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        for (int i = 0; i < 2; i++)
        {
            subBass = spectrum[i];
        }

        for (int i = 2; i < 6; i++)
        {
            bass = spectrum[i];
        }

        for (int i = 6; i < 12; i++)
        {
            lowerMidrange = spectrum[i];
        }

        for (int i = 12; i < 47; i++)
        {
            midRange = spectrum[i];
        }

        for (int i = 47; i < 93; i++)
        {
            upperMidrange = spectrum[i];
        }

        for (int i = 93; i < 140; i++)
        {
            presence = spectrum[i];
        }

        for (int i = 140; i < 465; i++)
        {
            brilliance = spectrum[i];
        }
    }
}
