using UnityEngine;
using System.Collections;

public class GetSamples : MonoBehaviour {

    private float[] samples;
    private float average;

    public void NormaliseAudio()
    {
        AudioClip clip = MusicManager.ImportedSong;
        samples = new float[clip.samples * clip.channels];
        average = 0;
        clip.GetData(samples, 0);

        for (int i = 0; i < samples.Length; i++)
        {
            average += Mathf.Abs(samples[i]);
        }

        average /= clip.samples;

        for (int i = 0; i < samples.Length; i++)
        {
            if (average > 0.08f && average < 0.11f)
            {
                samples[i] *= 3.1f;
            }
            else if (average >= 0.11f && average < 0.13f)
            {
                samples[i] *= 2.75f;
            }
            else if (average >= 0.13f && average < 0.15f)
            {
                samples[i] *= 2.4f;
            }
            else if (average >= 0.15f && average < 0.18f)
            {
                samples[i] *= 2.2f;
            }
            else if (average >= 0.18f && average < 0.22f)
            {
                samples[i] *= 1.65f;
            }
            else if (average >= 0.22f && average < 0.25f)
            {
                samples[i] *= 1.45f;
            }
            else if (average >= 0.25f && average < 0.28f)
            {
                samples[i] *= 1.25f;
            }
            else if (average >= 0.28f && average < 0.3f)
            {
                samples[i] *= 1.16f;
            }
            else if (average >= 0.4f && average < 0.43f)
            {
                samples[i] *= 0.9f;
            }
            else if (average >= 0.43f && average < 0.47f)
            {
                samples[i] *= 0.8f;
            }
            else if (average >= 0.47f && average < 0.5f)
            {
                samples[i] *= 0.73f;
            }
            else if (average >= 0.5f && average < 0.6f)
            {
                samples[i] *= 0.6f;
            }
            else if (average >= 0.6f)
            {
                samples[i] *= 0.5f;
            }

            while (samples[i] >= 1 || samples[i] <= -1)
                samples[i] /= 1.15f;
        }

        clip.SetData(samples, 0);
        MusicManager.ImportedSong = clip;

    }

    public void NormaliseDefaultAudio()
    {
        AudioClip clip = GetComponent<AudioSource>().clip;
        samples = new float[clip.samples * clip.channels];
        average = 0;
        clip.GetData(samples, 0);

        for (int i = 0; i < samples.Length; i++)
        {
            average += Mathf.Abs(samples[i]);
        }

        average /= clip.samples;

        for (int i = 0; i < samples.Length; i++)
        {
            if (average > 0.08f && average < 0.11f)
            {
                samples[i] *= 3.1f;
            }
            else if (average >= 0.11f && average < 0.13f)
            {
                samples[i] *= 2.75f;
            }
            else if (average >= 0.13f && average < 0.15f)
            {
                samples[i] *= 2.4f;
            }
            else if (average >= 0.15f && average < 0.18f)
            {
                samples[i] *= 2.2f;
            }
            else if (average >= 0.18f && average < 0.22f)
            {
                samples[i] *= 1.65f;
            }
            else if(average >= 0.22f && average < 0.25f)
            {
                samples[i] *= 1.45f;
            }
            else if(average >= 0.25f && average < 0.28f)
            {
                samples[i] *= 1.25f;
            }
            else if(average >= 0.28f && average < 0.3f)
            {
                samples[i] *= 1.16f;
            }
            else if (average >= 0.4f && average < 0.43f)
            {
                samples[i] *= 0.9f;
            }
            else if (average >= 0.43f && average < 0.47f)
            {
                samples[i] *= 0.8f;
            }
            else if (average >= 0.47f && average < 0.5f)
            {
                samples[i] *= 0.73f;
            }
            else if (average >= 0.5f && average < 0.6f)
            {
                samples[i] *= 0.6f;
            }
            else if (average >= 0.6f)
            {
                samples[i] *= 0.5f;
            }

            while (samples[i] >= 1 || samples[i] <= -1)
                samples[i] /= 1.15f;
        }

        clip.SetData(samples, 0);
        GetComponent<AudioSource>().clip = clip;
    }

}
