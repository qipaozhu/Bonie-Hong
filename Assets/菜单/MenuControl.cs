using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void StartHCM()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitHCM()
    {
        Application.Quit();
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
