using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Text progressText;
    private AsyncOperation async;
    private float progress;

   
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            async = SceneManager.LoadSceneAsync("TutorialLevel", LoadSceneMode.Single);
            async.allowSceneActivation = false;
            StartCoroutine(Loading());
        }
        else if (SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            async = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("TutorialLevel");
            async.allowSceneActivation = false;
            StartCoroutine(Loading());
        }
        
    }
    private IEnumerator Loading()
    {
        while (progress < 0.99)
        {
            progress = Mathf.Lerp(progress, async.progress / 9 * 10, Time.deltaTime);
            progressText.text = Mathf.Floor(progress * 100f).ToString() + "%";
            yield return null;
        }
        progress = 1f;
        async.allowSceneActivation = true;
    }
}
