using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    public static int characterSelected;
    [SerializeField]private GameObject[] characters;
    [SerializeField]private Transform indicator;
    [SerializeField]private Text shipName;

    void Start()
    {
        characterSelected = -1;

        indicator.position = new Vector3(0.0f, 1000.0f, 0.0f);
        shipName.text = "Default ship currently selected";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Selection();
        }

        shipName.color = new Color(shipName.color.r, shipName.color.g, shipName.color.b, Mathf.PingPong(Time.time, 1.0f));
    }

    void Selection()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (hit.collider.gameObject == characters[i].gameObject)
                {
                    indicator.position = hit.transform.position + new Vector3(0, 0.75f, 0);
                    characterSelected = System.Array.IndexOf(characters, hit.collider.gameObject);
                    shipName.text = hit.transform.name;
                }
            }
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }

}
