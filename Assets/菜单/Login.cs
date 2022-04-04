using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public static Login instance { get; private set; }
    public InputField passwdInput;

    public string passwd;

    public bool SubmitPasswd()
    {
        if (passwdInput.text == passwd)
        {
            return true;
        }
        else
        {
            passwdInput.text = "";
            SoundHelper.Beep();
            return false;
        }
    }

    public void MenuGO()
    {
        if (SubmitPasswd())
        {
            this.gameObject.SetActive(false);
            MenuControl.instance.StartHCM();
        }
    }
}
