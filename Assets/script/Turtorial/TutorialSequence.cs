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
        [SerializeField] private GameObject cookerUI;
        [SerializeField] private Sprite flourIcon;
        [SerializeField] private Sprite cheeseIcon;
        [SerializeField] private Button[] materialSlots;
        

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
            //Debug.LogError(playerName);
            playerText.SetText(playerName);
            //TODO: Save
        }
        private void NextButtomClick()
        {

            //Debug.LogError(i);
            switch (i)
            {
                case 1:
                    SaveName();
                    break;
                case 2:
                    nextButtom.gameObject.SetActive(false);
                    for (int i = 0; i < otherUI.Length; i++)
                    {
                        otherUI[i].SetActive(true);
                    }
                    break;
                case 3:
                    Time.timeScale = 1;
                    tutorialUI.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    break;
                case 5:
                    nextButtom.transform.localPosition = new Vector3(490, -460, 0);
                    break;
                case 7:
                    nextButtom.gameObject.SetActive(false);
                    tutorialUI.GetComponent<Image>().raycastTarget = false;
                    TutorialCooker();
                    break;
                default:
                    break;
            }
            i += 1;
            sequence[i].SetActive(true);
            sequence[i - 1].SetActive(false);
            
            
        }
        private void TutorialCooker()
        {
            cookerUI.transform.GetChild(0).GetComponent<Image>().sprite = flourIcon;
            cookerUI.transform.GetChild(0).GetComponent<Button>().enabled = false;
            for (int i = 0; i < 2; i++)
            {
                materialSlots[i].onClick.RemoveAllListeners();
                switch (i)
                {
                    case 0:
                        materialSlots[0].onClick.AddListener(delegate { AddMaterial(0); });
                        break;
                    case 1:
                        materialSlots[1].onClick.AddListener(delegate { AddMaterial(1); });
                        break;
                    default:
                        break;
                }

            }
           


        }
        private void AddMaterial(int index)
        {
            materialSlots[index].gameObject.GetComponent<Image>().sprite = cheeseIcon;
            switch (index)
            {
                case 0:
                    materialSlots[1].gameObject.GetComponent<Button>().enabled = false;
                    break;
                case 1:
                    materialSlots[0].gameObject.GetComponent<Button>().enabled = false;
                    break;
                default:
                    break;
            }
        }
        
        private void IsPlayerNextPot()
        {
            if (Cooker.isPlayerIn && i == 4)
            {
                NextButtomClick();
                nextButtom.transform.localPosition = new Vector3(800, -90, 0);
                nextButtom.gameObject.SetActive(true);
                PlayerMovement.isEnableInput = false;
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