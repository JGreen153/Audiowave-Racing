using UnityEngine;
using UnityEngine.SceneManagement;

public class CharInstantiation : MonoBehaviour
{

    [SerializeField]
    private GameObject[] characters;

    [SerializeField]
    private Vector3 spawnPosition;

    void OnEnable()
    {
        SceneManager.activeSceneChanged += InstantiatePlayer;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= InstantiatePlayer;
    }

    void InstantiatePlayer(Scene prev, Scene next)
    {
        int characterNumber = CharacterSelection.characterSelected;

        switch (next.buildIndex)
        {
            case 3:
            case 4:
                for (int i = 0; i < characters.Length; i++)
                {
                    if (characterNumber == i)
                    {
                        Instantiate(characters[i], spawnPosition, Quaternion.identity);
                        break;
                    }
                    else if (characterNumber < 0)
                    {
                        if (next.buildIndex == 3)
                            Instantiate(characters[0], spawnPosition, Quaternion.identity);
                        if (next.buildIndex == 4)
                            Instantiate(characters[1], spawnPosition, Quaternion.identity);
                        break;
                    }
                }
                break;
            case 5:
                for (int i = 0; i < characters.Length; i++)
                {
                    if (characterNumber == i)
                    {
                        Instantiate(characters[i], spawnPosition, Quaternion.Euler(0, 50.17f, 0));
                        break;
                    }
                    else if (characterNumber < 0)
                    {
                        Instantiate(characters[2], spawnPosition, Quaternion.Euler(0, 50.17f, 0));
                        break;
                    }
                }
                break;
        }
    }

}
