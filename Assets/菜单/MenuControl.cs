using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public GameObject pauseMenu;

    public static MenuControl instance { get; private set; }
    public GameObject load;
    public Text version;

    public void StartHCM() //��ʼ��û
    {
        load.SetActive(true);
        Invoke("LoadScene1", 1);
    }
    void LoadScene1() { SceneManager.LoadScene(1); }

    public void QuitHCM() //������û
    {
        Application.Quit();
    }
    public void BackMenu() //���ؿ�ʼ�˵�
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
    public void Pause() //��ͣ
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void NotPause() //ȡ����ͣ
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void NotPauseBak()
    {
        Time.timeScale = 1;
    }
    public void SeeWiki()
    {
        Application.OpenURL("https://github.com/qipaozhu/Bonie-Hong/wiki");
    }
    public void SeeCode()
    {
        Application.OpenURL("https://github.com/qipaozhu/Bonie-Hong");
    }
    public void SeeDownTool()
    {
        Application.OpenURL("https://docs.qq.com/doc/DZGF0SVRoUWR3Rkpx");
    }


    void Start()
    {
        instance = this;

        if (version != null) { version.text = Application.version; }
    }
}
