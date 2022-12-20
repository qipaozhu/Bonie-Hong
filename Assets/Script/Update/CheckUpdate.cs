using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class CheckUpdate : MonoBehaviour
{
    public Slider jindu;
    public Button clickButton;
    public Text contText;
    string newverconfigFilePath() { return Application.temporaryCachePath + "/newverinfo.hcm"; }

    string Sharelink;

    void Start()
    {
        WebClient wc = new WebClient();
        contText.text = "��������......\n�������������ֱ�ӽ���";
        
        wc.DownloadFileAsync(new System.Uri("https://qipaozhu.github.io/Assets/BonieHong/NewVer.hcm"), newverconfigFilePath());
        wc.DownloadProgressChanged += WebClient_DownloadProgressChanged;
        wc.DownloadFileCompleted += CompleteDownload;
        
    }

    void CompleteDownload(object sender,AsyncCompletedEventArgs ace)
    {
        contText.text = "������������...";
        string[] verdata = File.ReadAllLines(newverconfigFilePath());

        Debug.Log(verdata + newverconfigFilePath());
        if (verdata[0] != Application.version)
        {
            if (Application.platform != RuntimePlatform.WindowsPlayer)
            {
                contText.text = "��⵽���£�\n��������Ҫ���°�";
                return;
            }
            contText.text = "��⵽���£�\n������ȷ�������������ظ���";
            clickButton.onClick.RemoveAllListeners();
            clickButton.onClick.AddListener(OpenDownLink);
            clickButton.GetComponentInChildren<Text>().text = "�򿪸���";

            Sharelink = verdata[1];
            
        }
        else
        {
            contText.text = "�Ѿ������°汾��";
        }
    }
    
    public void OpenDownLink()
    {
        Debug.Log(Sharelink);
        Application.OpenURL(Sharelink);
    }

    private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        jindu.value = e.ProgressPercentage;
    }
}
