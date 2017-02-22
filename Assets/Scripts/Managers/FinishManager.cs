using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FinishManager : MonoBehaviour {

    [SerializeField]
    private GameObject finishButton;

    [SerializeField]
    private PositionManager PosManager;

    [SerializeField]
    private CameraOrbit orbitCam;
    [SerializeField]
    private CameraControl cam;

    private string displayedText = "";

    private Text text = null;

    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        LapManager.OnUpdateFinishManager += Display;
        LapManager.OnUpdateFinishManager += ShowText;
    }

    void Display()
    {
        cam.enabled = false;
        orbitCam.enabled = true;
        orbitCam.Target = cam.Player;
        orbitCam.Target.GetComponent<PlayerMovement>().enabled = false;
        orbitCam.Target.GetComponent<AIMovement>().enabled = true;

        finishButton.SetActive(true);
    }

    void ShowText()
    {
        for (int i = 0; i < PosManager.Racers.Count; i++)
        {
            int pos = i + 1;

            displayedText = displayedText.ToString() + pos + " | " +
                PosManager.Racers[i].gameObject.name + "\n" + "\n";
        }
        text.text = displayedText;

        PosManager.enabled = false;
    }

    void OnDisable()
    {
        LapManager.OnUpdateFinishManager -= Display;
        LapManager.OnUpdateFinishManager -= ShowText;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
