using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PositionManager : MonoBehaviour {

	[SerializeField]private List<PositioningScript> racers;

    private PositioningScript player;

    private int prevplayerPos;
    private int currentPlayerPos;

    private int position;
    private int amountOfCars;

    private Text text;

    public List<PositioningScript> Racers { get { return racers; } }

    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
        amountOfCars = racers.Count;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PositioningScript>();
        racers[0] = player;
    }

    // Update is called once per frame
    void Update()
    {
        racers.Sort();

        CheckPosition();

        if (prevplayerPos != currentPlayerPos)
            UpdateText();
    }

    void CheckPosition()
    {
        prevplayerPos = currentPlayerPos;

        currentPlayerPos = racers.IndexOf(player);

        position = currentPlayerPos + 1;
    }

    void UpdateText()
    {
        text.text = "POSITION: " + position + "/" + amountOfCars;
    }

}
