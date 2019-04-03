using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private List<Card> cards;

    [SerializeField]
    private int percentTowardNextCard = 0;

    private UIManager uiManager;

    void Start() {
        uiManager = (UIManager) FindObjectOfType(typeof(UIManager));
    }

    void OnEnable() {
        NotifyUIManagerCards();
    }

    void OnValidate() {
        NotifyUIManagerCards();
    }

    private void NotifyUIManagerCards() {
        if (uiManager != null) {
            uiManager.SetCards(cards);
        }
    }
}
