using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {

    [SerializeField]private AudioMixerGroup masterMixer;
    [SerializeField]private Slider musicSlider;
    [SerializeField]private Slider effectsSlider;

    // Use this for initialization
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("mainMusicVolume");
        effectsSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void MusicVolume()
    {
        masterMixer.audioMixer.SetFloat("musicVol", musicSlider.value);
        PlayerPrefs.SetFloat("mainMusicVolume", musicSlider.value);
    }

    public void EffectsVolume()
    {
        masterMixer.audioMixer.SetFloat("sfxVol", effectsSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", effectsSlider.value);
    }

}
