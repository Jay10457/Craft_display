using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tutorial;
namespace Tutorial
{


    public class SkillCoutdown : MonoBehaviour
    {
        [SerializeField] private Image cdProgress;
        [SerializeField] private Text timeText;

        private float time = 10;

        private void OnEnable()
        {
            timeText.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (!PlayerMovement.isSkillOk)
            {
                CoolDown();
            }
       
            
           
            
        }
        private void CoolDown()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                cdProgress.fillAmount = time / 10;
                
                if (cdProgress.fillAmount < 1)
                {
                    timeText.gameObject.SetActive(true);
                    timeText.text = time.ToString("0");
                }
                if (cdProgress.fillAmount == 0)
                {
                    
                    PlayerMovement.isSkillOk = true;
                    timeText.gameObject.SetActive(false);
                    time = 10;
                }
                
            }
        }
        
    }
}
