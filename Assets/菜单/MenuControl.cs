using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject pauseMenu;
    public static MenuControl instance { get; private set; }

    public int prop1AddHealth;

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
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void NotPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    void Start()
    {
        instance = this;
        SceneManager.UnloadSceneAsync(1);
    }

    //”√“≈’’
    public void UseDP()
    {
        if (PlayerCollect.instance.Prop1Conut < 1 || PlayerCollect.instance.PnowHealth >= PlayerCollect.instance.PmaxHealth)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        PlayerCollect.instance.ChangeHealth(prop1AddHealth);
        PlayerCollect.instance.SetProp(1, -1);
    }

}
