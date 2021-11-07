using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
namespace Tutorial
{


    public class TutorialSequence : MonoBehaviour
    {
        [SerializeField] private GameObject[] otherUI;
        [SerializeField] private GameObject[] sequence;
        [SerializeField] private GameObject tutorialUI;
        [SerializeField] private Button nextButtom;
        [SerializeField] private Texture2D cursor;
        [SerializeField] private InputField nameInput;
        [SerializeField] private TMP_Text playerText;

        private event Action OnNextButtomClick;
        private GameObject currentChapter;
        private Image bg;
        private string playerName;
        private int i = 0;

        private void OnEnable()
        {
            nextButtom.onClick.RemoveAllListeners();
            nextButtom.onClick.AddListener(() =>
            {
                if (OnNextButtomClick != null) OnNextButtomClick();
            });
        }
        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            OnNextButtomClick += NextButtomClick;
            
            
        }
        private void Update()
        {
            CheckName();
            UnLock();
            IsPlayerNextPot();
            
        }
        private void CheckName()
        {
            if (i == 1 && nameInput.text == "")
            {
                
                nextButtom.interactable = false;
            }
            else
            {
                nextButtom.interactable = true;
                playerName = nameInput.text;
            }
        }
        private void UnLock()
        {
            if (i == 3 && Input.anyKeyDown)
            {
                NextButtomClick();
                
            }
        }
        private void SaveName()
        {
            Debug.LogError(playerName);
            playerText.SetText(playerName);
            //TODO: Save
        }
        private void NextButtomClick()
        {

            if (i == 1)
            {
                SaveName();
            }
            if (i == 2)
            {
                nextButtom.gameObject.SetActive(false);
                for (int i = 0; i < otherUI.Length; i++)
                {
                    otherUI[i].SetActive(true);
                }
            }
            if (i == 3)
            {
                Time.timeScale = 1;
                tutorialUI.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                
            }
            i += 1;
            sequence[i].SetActive(true);
            sequence[i - 1].SetActive(false);
            
            
        }

        private void IsPlayerNextPot()
        {
            if (Cooker.isPlayerIn && i == 4)
            {
                NextButtomClick();
                nextButtom.gameObject.SetActive(true);
            }
        }

        private void Init()
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
            //tutorialUI.SetActive(true);
            if (tutorialUI.activeInHierarchy)
            {
                for (int i = 0; i < otherUI.Length; i++)
                {
                    otherUI[i].SetActive(false);
                }
                Time.timeScale = 0;
            }
        }
    }
}