using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour {

    [SerializeField]private int level;
    private Button button;

	// Use this for initialization
	void Start ()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => MusicManager.instance.LoadMethod(level));
	}
}
