using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private List<Card> cards;

    private UIManager uiManager;

    void Start() {
        uiManager = (UIManager) FindObjectOfType(typeof(UIManager));
        Debug.Log("found ui manager: " + uiManager);
    }

    void OnEnable() {
        NotifyUIManagerCards();
    }

    private void NotifyUIManagerCards() {
        
    }
}
