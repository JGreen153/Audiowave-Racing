using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
    [SerializeField]private MainMenuCamera cameraScript;
    [SerializeField]private Transform cameraMount;
    [SerializeField]private Transform mainMenuMount;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start()
    {
        CountdownManager.canDrive = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

