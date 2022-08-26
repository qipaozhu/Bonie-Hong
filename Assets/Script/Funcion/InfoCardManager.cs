using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("BrTool/InfoCardManager")]
public class InfoCardManager : MonoBehaviour
{
    public static InfoCardManager main { get; private set; }

    [Header("”Œœ∑∂‘œÛ")]
    public LayoutGroup textShowPlace;
    public Animator infoWindowAni;
    public Image backgound;

    public GameObject textPrefab;

    void Start()
    {
        main = this;
    }

    public void HideCard()
    {
        infoWindowAni.SetBool("Hide",true);
        backgound.enabled = false;
        backgound.gameObject.GetComponent<Button>().enabled = false;
        for (int i = 0; i < textShowPlace.transform.childCount; i++)
        {
            Destroy(textShowPlace.transform.GetChild(i).gameObject);
        }
    }

    public void ShowInfomation(string[] text)
    {
        for (int i = 0; i < textShowPlace.transform.childCount; i++)
        {
            Destroy(textShowPlace.transform.GetChild(i).gameObject);
        }
        infoWindowAni.SetBool("Hide", false);
        backgound.enabled = true;

        for (int i = 0; i < text.Length; i++)
        {
            Text a = Instantiate(textPrefab, textShowPlace.transform, false).GetComponent<Text>();
            
            a.text = text[i];            
        }
        backgound.gameObject.GetComponent<Button>().enabled = true;
    }
}
