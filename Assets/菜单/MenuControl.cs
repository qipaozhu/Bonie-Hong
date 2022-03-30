using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject pauseMenu;
    public static MenuControl instance { get; private set; }

    public int prop1AddHealth;

    public void StartHCM() //开始出没
    {
        SceneManager.LoadScene(1);
    }
    public void QuitHCM() //结束出没
    {
        Application.Quit();
    }
    public void BackMenu() //返回开始菜单
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
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
        SceneManager.UnloadSceneAsync(1);
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
