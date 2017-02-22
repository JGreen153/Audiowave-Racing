using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneModifications : MonoBehaviour {

    void OnEnable()
    {
        SceneManager.activeSceneChanged += HeightSceneModifications;
        SceneManager.activeSceneChanged += AISceneModifications;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= HeightSceneModifications;
        SceneManager.activeSceneChanged -= AISceneModifications;
    }

    void HeightSceneModifications(Scene scene, Scene newScene)
    {
        switch (newScene.buildIndex)
        {
            case 3:
                HoverScript.hoverHeight = 13;
                break;
            case 4:
                HoverScript.hoverHeight = 7;
                break;
            case 5:
                HoverScript.hoverHeight = 9;
                break;
        }
    }

    void AISceneModifications(Scene scene, Scene newScene)
    {
        switch (newScene.buildIndex)
        {
            case 3:
                AIMovement.distance = 50;
                AIMovement.speedLimit = 170;
                AIMovement.accel = 35;
                AIMovement.turnSpeed = 300;
                break;
            case 4:
                AIMovement.distance = 40;
                AIMovement.speedLimit = 130;
                AIMovement.accel = 15;
                AIMovement.turnSpeed = 125;
                break;
            case 5:
                AIMovement.distance = 40;
                AIMovement.speedLimit = 170;
                AIMovement.accel = 25;
                AIMovement.turnSpeed = 150;
                break;
        }
    }

}
