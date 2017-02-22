using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Analyse : MonoBehaviour {

    private ReadAudioData data;
    private GameObject[] cubes;
    private int numOfCubes;

    [SerializeField]
    private GameObject cube;

    [SerializeField]
    private float scale;


    void Start()
    {
        data = GetComponent<ReadAudioData>();

        for (int y = 0; y < 7; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                Instantiate(cube, new Vector3(x, y, 30.0f), Quaternion.identity);
            }
        }

        cubes = GameObject.FindGameObjectsWithTag("Cubes");

        numOfCubes = cubes.Length;

    }

    void Update()
    {
        for (int i = 0; i < numOfCubes; i++)
        {
            Vector3 currentScale = cubes[i].transform.localScale;

            Color col = cubes[i].GetComponent<Renderer>().material.color;

            switch ((int)cubes[i].transform.position.y)
            {
                case 0:
                    currentScale.x = data.SubBass * scale;
                    break;
                case 1:
                    currentScale.x = data.Bass * scale;
                    break;
                case 2:
                    currentScale.x = data.LowerMidRange * scale;
                    break;
                case 3:
                    currentScale.x = data.MidRange * scale;
                    break;
                case 4:
                    currentScale.x = data.UpperMidRange * scale;
                    break;
                case 5:
                    currentScale.x = data.Presence * scale;
                    break;
                case 6:
                    currentScale.x = data.Brilliance * scale;
                    break;
            }

            cubes[i].transform.localScale = currentScale;

            col = Color.Lerp(Color.white, Color.red, currentScale.x);

            cubes[i].GetComponent<Renderer>().material.color = col; 
        }
    }

}

