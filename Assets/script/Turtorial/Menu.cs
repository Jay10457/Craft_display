using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
namespace Tutorial
{

    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button tutorialButtom;
        [SerializeField] private Button storyButtom;
        [SerializeField] private GameObject loadingPanel;

        private event Action OnTutorialButtomClick;

        private void OnEnable()
        {
            tutorialButtom.onClick.RemoveAllListeners();
            tutorialButtom.onClick.AddListener(() =>
            {
                if (OnTutorialButtomClick != null) OnTutorialButtomClick();
            });
        }
        private void Start()
        {
            OnTutorialButtomClick += ToLoading;
        }
        private void ToLoading()
        {
            loadingPanel.SetActive(true);
        }
    }
}
