using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoostUI : MonoBehaviour {

    private BoostManager boostManager;
    private Image boostUI;
    private Text boostText;

	// Use this for initialization
	void Start ()
    {
        boostManager = FindObjectOfType<BoostManager>();
        boostUI = GetComponent<Image>();
        boostText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Color textColour = boostText.color;

        if (!boostManager.BoostIsFull)
        {
            boostUI.fillAmount = boostManager.Timer / 10.0f;
            textColour.a -= Time.smoothDeltaTime;
            boostText.color = textColour;
        }
        else
        {
            boostText.color = new Color(boostText.color.r, boostText.color.g, boostText.color.b, Mathf.PingPong(Time.time, 1.0f));
        }

        if (boostManager.BoostIsFull && boostUI.fillAmount > 0)
        {
            boostUI.fillAmount -= 0.75f * Time.smoothDeltaTime;
        }
    }
}
