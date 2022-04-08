using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl_human : MonoBehaviour
{
    public GameObject pauseMenu;

    public static MenuControl_human instance { get; private set; }
    public int prop1AddHealth;
    public GameObject load;

    private void Awake()
    {
        instance = this;
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
    public void SeeCode()
    {
        Application.OpenURL("https://github.com/qipaozhu/Bonie-Hong");
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
        if (PlayerCollect.instance.Prop2Conut <= 0)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.OK();
        HlinControl_human.instance.SetSpeed();
        PlayerCollect.instance.SetProp(2, -1);
    }

    //用秋键
    public void UseDJR()
    {
        if (PlayerCollect.instance.Prop3Conut <= 0)
        {
            SoundHelper.Beep();
            return;
        }
        SoundHelper.DjrSay();
        SoundHelper.OK();
        PlayerCollect.instance.AddSpeed();
        PlayerCollect.instance.SetProp(3, -1);
    }
}
