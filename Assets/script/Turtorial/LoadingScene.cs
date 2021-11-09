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
        async = SceneManager.LoadSceneAsync("TutorialLevel");
        async.allowSceneActivation = false;
        StartCoroutine(Loading());
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
