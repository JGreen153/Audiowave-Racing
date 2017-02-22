using UnityEngine;
using UnityEngine.UI;

public class LapManager : MonoBehaviour {

    public delegate void UpdateFinishManager();
    public static event UpdateFinishManager OnUpdateFinishManager;

	public static int currentLap = 1;

    private FinishManager finishManager;
    private PositioningScript player;

    private Text text;

    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
        finishManager = FindObjectOfType<FinishManager>();

        currentLap = 1;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PositioningScript>();
        text.text = "LAP: " + currentLap + "/" + "3";
    }

    void OnEnable()
    {
        PositioningScript.OnUpdateLapText += UpdateText;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.LapNo == 3)
        {
            finishManager.enabled = true;
            player.gameObject.name = "Player";

            if (OnUpdateFinishManager != null)
                OnUpdateFinishManager();

            enabled = false;
        }
    }

    void OnDisable()
    {
        PositioningScript.OnUpdateLapText -= UpdateText;
    }

    void UpdateText()
    {
        text.text = "LAP: " + currentLap + "/" + "3";
    }
}
