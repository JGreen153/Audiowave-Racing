using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [SerializeField]private AudioMixerGroup masterMixer;
    [SerializeField]private Slider audioVolumeSlider;
    [SerializeField]private Slider effectsVolumeSlider;
    private Canvas canvas;
    private AudioSource mainAudio;
    private AudioSource pauseAudio; 
	
	// Update is called once per frame
    void Start()
    {
        canvas = GetComponent<Canvas>();
        pauseAudio = GetComponent<AudioSource>();
        mainAudio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }

	void Update () {

        if (Input.GetButtonDown("Cancel"))
        {

            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            canvas.enabled = !canvas.enabled;

            if (canvas.enabled)
            {
                mainAudio.Pause();
                pauseAudio.Play();
            }
            else if(!canvas.enabled)
            {
                pauseAudio.Stop();
                mainAudio.Play();
            }

        }	
	}

    public void Resume()
    {
        Time.timeScale = 1;
        pauseAudio.Stop();
        mainAudio.Play();
        canvas.enabled = false;
    }

    public void UnFreeze()
    {
        Time.timeScale = 1;
    }

    public void AdjustAudioVolume()
    {
        masterMixer.audioMixer.SetFloat("musicVol", audioVolumeSlider.value);
    }

    public void AdjustEffectsVolume()
    {
        masterMixer.audioMixer.SetFloat("sfxVol", effectsVolumeSlider.value);
    }

}
