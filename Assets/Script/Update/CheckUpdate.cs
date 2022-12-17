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
        if(Application.platform != RuntimePlatform.WindowsPlayer)
        {
            contText.text = "请及时询问是否有更新";
            return;
        }
        WebClient wc = new WebClient();
        contText.text = "检查更新中......\n太久没响应说明连接超时";
        
        wc.DownloadFileAsync(new System.Uri("https://qipaozhu.github.io/Assets/BonieHong/NewVer.hcm"), newverconfigFilePath());
        wc.DownloadProgressChanged += WebClient_DownloadProgressChanged;
        wc.DownloadFileCompleted += CompleteDownload;
        
    }

    void CompleteDownload(object sender,AsyncCompletedEventArgs ace)
    {
        contText.text = "获取配置中";
        string[] verdata = File.ReadAllLines(newverconfigFilePath());

        Debug.Log(verdata + newverconfigFilePath());
        if (verdata[0] != Application.version)
        {
            contText.text = "检测到更新！\n单击“确定”打开链接下载更新";
            clickButton.onClick.RemoveAllListeners();
            clickButton.onClick.AddListener(OpenDownLink);
            clickButton.GetComponentInChildren<Text>().text = "打开更新";

            Sharelink = verdata[1];
            
        }
        else
        {
            contText.text = "已经是最新版本！";
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
