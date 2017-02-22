using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour {

    public static bool canDrive;

	private float countDown;

	private Text text;

    void Awake()
    {
        canDrive = false;
    }

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();

        countDown = 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("StartCountDown", 2.0f);

        if (countDown > 2.5f && countDown <= 3.5f)
        {
            text.text = "" + 3;
        }
        else if (countDown <= 2.5f && countDown > 1.5f)
        {
            text.text = "" + 2;
        }
        else if (countDown <= 1.5f && countDown > 0.5)
        {
            text.text = "" + 1;
        }
        else if (countDown <= 0.5f && countDown > 0)
        {
            text.text = "GO!!!";
        }
        else
        { 
            text.text = "";
        }

        if (countDown <= 0.5f)
        {
            canDrive = true;
        }

        if(countDown <= 0)
        {
            enabled = false;
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void StartCountDown()
    {
        countDown -= Time.smoothDeltaTime;
    }
}
