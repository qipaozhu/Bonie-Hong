using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text dialogText;

    public void CloseWindows()
    {
        Destroy(gameObject);
    }
    public void SetDialogText(string text)
    {
        dialogText.text = text;
    }
}
