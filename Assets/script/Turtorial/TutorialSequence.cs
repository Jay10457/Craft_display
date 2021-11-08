using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Inventory;
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
        [SerializeField] private Text playerText;
        [SerializeField] private GameObject cookerUI;
        [SerializeField] private Sprite flourIcon;
        [SerializeField] private Sprite cheeseIcon;
        [SerializeField] private Image cheeseAmountIcon;
        [SerializeField] private Button[] materialSlots;
        [SerializeField] private Button cookStartButtom;
        [SerializeField] private Text materialAmoutText;
        [SerializeField] private Image progressBar;
        [SerializeField] private Button cookDone;
        [SerializeField] private Item jchDish;
        [SerializeField] private Camera cam;
        
        

        private event Action OnNextButtomClick;
        private event Action OnCookStartButtomClick;
        private event Action OnCookDoneButtomClick;
        private GameObject currentChapter;
        
        private Image bg;
        private string playerName;
        private int materialAmount = 5;       
        private int i = 0;
        private float time = 0;
        private bool canCook = false;
        public static bool lookAtTarget = false;
        
        

        private void OnEnable()
        {
            nextButtom.onClick.RemoveAllListeners();
            nextButtom.onClick.AddListener(() =>
            {
                if (OnNextButtomClick != null) OnNextButtomClick();
            });
            cookStartButtom.onClick.RemoveAllListeners();
            cookStartButtom.onClick.AddListener(() =>
            {
                if (OnCookStartButtomClick != null) OnCookStartButtomClick();
            });
            cookDone.onClick.RemoveAllListeners();
            cookDone.onClick.AddListener(() =>
            {
                if (OnCookDoneButtomClick != null) OnCookDoneButtomClick();
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
            CookButtonController();
            MaterialAmount();
            if (canCook)
            {
                CookerTimer(3f);
            }
            

           


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
            playerText.text = playerName;
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
                    
                    TutorialCooker();
                    StartCoroutine(Timer(1f));// into pot tutorial
                    break;
                case 8:
                    tutorialUI.GetComponent<Image>().raycastTarget = false;
                    break;
                case 9:
                    StartCoroutine(Timer(1f));
                    
                    break;
                default:
                    break;
            }
            i += 1;
            sequence[i].SetActive(true);
            sequence[i - 1].SetActive(false);
            
            
        }
        #region cooker
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
            OnCookStartButtomClick += CookStart;
            OnCookDoneButtomClick += AddFoodToSlot;
           


        }
        private void MaterialAmount()
        {
            materialAmoutText.text = string.Format("{0}/5", materialAmount);
        }
        private void AddMaterial(int index)
        {
            materialSlots[index].gameObject.GetComponent<Image>().sprite = cheeseIcon;
            materialAmount -= 1;
            StartCoroutine(FrashIcon(2f));
            StartCoroutine(Timer(0f));
            switch (index)
            {
                case 0:
                    materialSlots[1].gameObject.GetComponent<Button>().enabled = false;
                    materialSlots[0].gameObject.GetComponent<Button>().enabled = false;
                    break;
                case 1:
                    materialSlots[0].gameObject.GetComponent<Button>().enabled = false;
                    materialSlots[1].gameObject.GetComponent<Button>().enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void CookButtonController()
        {
            
            if (materialSlots[0].GetComponent<Image>().sprite.name == "Game_Cooking_cheese" 
                || materialSlots[1].GetComponent<Image>().sprite.name == "Game_Cooking_cheese" )
            {
                if (sequence[11].activeInHierarchy)
                {
                    cookStartButtom.interactable = true;
                }
               
            }
            else
            {
                cookStartButtom.interactable = false;
            }
            
        }
        private void CookStart()
        {
            cookerUI.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(true);
            canCook = true;
            NextButtomClick();
            
            
        }
        private void CookerTimer(float wantTime)
        {
            
            if (time < wantTime)
            {
                time += Time.deltaTime;
                progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = time / wantTime;
            }
            if (progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount == 1 && i == 12)
            {
                NextButtomClick();
                cookDone.gameObject.SetActive(true);
                progressBar.gameObject.SetActive(false);

            }

        }
        #endregion
        private void AddFoodToSlot()
        {
            cookDone.gameObject.SetActive(false);
            InventoryManager.AddItemToInventory(jchDish, 1);
            PlayerMovement.isEnableInput = true;
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
        private IEnumerator Timer(float time)
        {
            yield return new WaitForSeconds(time);
            NextButtomClick();
            
            
            
            
        }
        private IEnumerator FrashIcon(float time)
        {
            cheeseAmountIcon.gameObject.SetActive(true);
            yield return new WaitForSeconds(time);

            cheeseAmountIcon.gameObject.SetActive(false);
        }
    }
}