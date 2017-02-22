using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance = null;

    [SerializeField]private AudioClip[] racingSongs;
    [SerializeField]private AudioClip menuSong;

    private static AudioClip importedSong;
    private static AudioClip defaultSong;
    private AudioSource backgroundMusic;

    public static AudioClip ImportedSong
    {
        get { return importedSong; }
        set { importedSong = value; }
    }

    private ReadAudioData data;
    private GetSamples samples;

    private Image loadingScreen;
    private Text loadingText;

    private bool shouldPlay;
    private bool shouldModify;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        backgroundMusic = GetComponent<AudioSource>();

        data = GetComponent<ReadAudioData>();

        samples = GetComponent<GetSamples>();

        loadingScreen = GetComponentInChildren<Image>();
        loadingText = GetComponentInChildren<Text>();
        loadingScreen.gameObject.SetActive(false);

        importedSong = null;

        shouldModify = true;

        shouldPlay = true;
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneSwitched;
        SceneManager.activeSceneChanged += DefaultSong;
    }

    void Update ()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(sceneIndex == 0 && !shouldPlay)
        {
            shouldPlay = true;
        }
        if (backgroundMusic.clip != null && !backgroundMusic.isPlaying && backgroundMusic.clip.loadState == AudioDataLoadState.Loaded && shouldPlay)
        {
            backgroundMusic.Play();
            shouldPlay = false;
        }

        if (sceneIndex > 2 && importedSong != null) { backgroundMusic.clip = importedSong; }
        else if(sceneIndex > 2 && importedSong == null) { backgroundMusic.clip = defaultSong; }
        else if (sceneIndex <= 2) { backgroundMusic.clip = menuSong; }
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneSwitched;
        SceneManager.activeSceneChanged -= DefaultSong;
    }

    void DefaultSong(Scene scene, Scene newScene)
    {
        if (importedSong == null)
        {
            if (newScene.buildIndex == 3 && shouldModify)
            {
                defaultSong = racingSongs[0];
                samples.NormaliseDefaultAudio();
                shouldModify = false;
            }
            else if (newScene.buildIndex == 4 && shouldModify)
            {
                defaultSong = racingSongs[1];
                samples.NormaliseDefaultAudio();
                shouldModify = false;
            }
            else if (newScene.buildIndex == 5 && shouldModify)
            {
                defaultSong = racingSongs[2];
                samples.NormaliseDefaultAudio();
                shouldModify = false;
            }
        }
    }

    public void LoadMethod(int level)
    {
        StartCoroutine("Load", level);
    }

    IEnumerator Load(int level)
    {
        yield return StartCoroutine("FadeInLoadingScreen");

        yield return StartCoroutine("FadeOut");

        yield return SceneManager.LoadSceneAsync(level);

        yield return StartCoroutine("FadeOutLoadingScreen");

        yield return StartCoroutine("FadeIn");
    }

    IEnumerator FadeOut()
    {
        float t = 0.0f;
        float fadeTime = 1.0f;

        while (t < fadeTime)
        {
            backgroundMusic.volume = Mathf.Lerp(1.0f, 0.0f, t / fadeTime);

            t += Time.smoothDeltaTime;
            yield return null;
        }

        if (backgroundMusic.volume < 0.15f)
            backgroundMusic.volume = 0.0f;

    }

    IEnumerator FadeIn()
    {
        float t = 0.0f;
        float fadeTime = 1.0f;

        while (t < fadeTime)
        {
            backgroundMusic.volume = Mathf.Lerp(0.0f, 1.0f, t / fadeTime);

            t += Time.smoothDeltaTime;
            yield return null;
        }

        if (backgroundMusic.volume > 0.95f)
            backgroundMusic.volume = 1.0f;
    }

    IEnumerator FadeInLoadingScreen()
    {
        float t = 0.0f;
        float fadeTime = 1.0f;
        Color colour = loadingScreen.color;
        Color textColour = loadingText.color;
        loadingScreen.gameObject.SetActive(true);

        while (t < fadeTime)
        {
            colour.a = Mathf.Lerp(0.0f, 1.0f, t / fadeTime);
            textColour.a = Mathf.Lerp(0.0f, 1.0f, t / fadeTime);

            loadingScreen.color = colour;
            loadingText.color = textColour;

            t += Time.smoothDeltaTime;
            yield return null;
        }

    }

    IEnumerator FadeOutLoadingScreen()
    {
        float t = 0.0f;
        float fadeTime = 1.0f;
        Color colour = loadingScreen.color;
        Color textColour = loadingText.color;

        while (t < fadeTime)
        {
            colour.a = Mathf.Lerp(1.0f, 0.0f, t / fadeTime);
            textColour.a = Mathf.Lerp(1.0f, 0.0f, t / fadeTime);

            loadingScreen.color = colour;
            loadingText.color = textColour;

            t += Time.smoothDeltaTime;
            yield return null;
        }

        loadingScreen.gameObject.SetActive(false);

    }

    void OnSceneSwitched(Scene scene, Scene newScene)
    {
        shouldPlay = true;

        if (newScene.buildIndex > 2)
        {
            shouldModify = true;
            data.enabled = true;
        }
        else if (data != null && newScene.buildIndex <= 2)
        {
            data.enabled = false;
        }
    }

}
