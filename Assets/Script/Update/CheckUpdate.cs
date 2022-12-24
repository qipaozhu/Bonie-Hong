using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class CheckUpdate : MonoBehaviour
{
    public Button ToUpdate;
    string newverconfigFilePath() { return Application.temporaryCachePath + "/newverinfo.hcm"; }

    string Sharelink;

    void Start()
    {
        WebClient wc = new WebClient();

        
        wc.DownloadFileAsync(new System.Uri("https://qipaozhu.github.io/Assets/BonieHong/NewVer.hcm"), newverconfigFilePath());
        wc.DownloadFileCompleted += CompleteDownload;
        
    }

    void CompleteDownload(object sender,AsyncCompletedEventArgs ace)
    {
        string[] verdata = File.ReadAllLines(newverconfigFilePath());

        Debug.Log(verdata + newverconfigFilePath());
        if (verdata[0] != Application.version)
        {
            if (Application.platform != RuntimePlatform.WindowsPlayer)
            {
                ToUpdate.GetComponentInChildren<Text>().text = "检测到更新！\n请向作者要最新版";
                return;
            }
            ToUpdate.GetComponentInChildren<Text>().text = "检测到更新！\n单击“确定”打开链接下载更新";
            ToUpdate.onClick.AddListener(OpenDownLink);

            Sharelink = verdata[1];
            
        }
        else
        {
            ToUpdate.GetComponentInChildren<Text>().text = "已经是最新版本！";
        }
    }
    
    public void OpenDownLink()
    {
        Debug.Log(Sharelink);
        Application.OpenURL(Sharelink);
    }
}
