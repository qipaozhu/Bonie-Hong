using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public GameObject pauseMenu;

    public static MenuControl instance { get; private set; }
    public int prop1AddHealth;
    public GameObject load;
    public Text version;

    public void StartHCM() //开始出没
    {
        load.SetActive(true);
        Invoke("LoadScene1", 1);
    }void LoadScene1() { SceneManager.LoadScene(1); }

    public void StartHCMHumanMode() //开始出没
    {
        load.SetActive(true);
        Invoke("LoadScene2", 1);
    }void LoadScene2() { SceneManager.LoadScene(2); }

    public void QuitHCM() //结束出没
    {
        Application.Quit();
    }
    public void BackMenu() //返回开始菜单
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
    public void Pause() //暂停
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void NotPause() //取消暂停
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void NotPauseBak(){
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
        if(SceneManager.sceneCount == 2) SceneManager.UnloadSceneAsync(1);
        if (System.IO.File.Exists("isReg.txt") && SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0))
        {
            Debug.Log("没有注册");
            GameObject.Find("Login").SetActive(true);
        }
        version.text = Application.version;
    }

    //用遗照
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

    //用个人信息
    public void UseIF()
    {
        if (!CenterCtrl.instance.isHCM)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        HlinControl.instance.SetSpeed();
        PlayerCollect.instance.SetProp(2, -1);
    }
}
