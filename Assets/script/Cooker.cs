using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooker : MonoBehaviour
{
    [SerializeField] private Canvas cookUI;
    private List<Collider> playerColliders = new List<Collider>();


    private void OnTriggerEnter(Collider col)
    {
        
        
        if (!playerColliders.Contains(col) && col.gameObject.tag == "Player")
        {
            cookUI.gameObject.SetActive(true);
            playerColliders.Add(col);
            //Debug.LogError(playerColliders.Count);
            
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerColliders.Remove(col);
            cookUI.gameObject.SetActive(false);
            //Debug.LogError(playerColliders.Count);
        }
       
    }
}
