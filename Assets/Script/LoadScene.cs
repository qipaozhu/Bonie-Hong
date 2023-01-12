using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;

    public GameObject loading;
    public Image loadBar;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    public void ToLoadScene(int id)
    {
        StartCoroutine(LoadScenes(id));
    }

    IEnumerator LoadScenes(int id)
    {
        loading.SetActive(true);
        AsyncOperation ao = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while (true)
        {
            yield return null;
            loadBar.fillAmount = ao.progress;
            if (ao.progress == 1)
            {
                loading.SetActive(false);
                loadBar.fillAmount = 0;
                break;
            }
        }
    }
}
