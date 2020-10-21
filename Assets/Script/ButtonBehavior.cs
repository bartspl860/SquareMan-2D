using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{

    [SerializeField]
    public AudioSource theme;
    [SerializeField]
    private Transform camera;
    public void ChangeToScene(string sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }    
    public void ExitGame()
    {
        Application.Quit();
    }
    public void CameraToControls()
    {
        camera.position = new Vector3(135f, -100f, -10f);
    }
    public void CameraToMenu()
    {
        camera.position = new Vector3(135f, 84f, -10f);
    }
}

