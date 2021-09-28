using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void BotonStart()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonQuit()
    {
        Debug.Log("Quitamos la aplicación");
        Application.Quit();
    }
}