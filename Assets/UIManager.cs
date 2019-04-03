using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Image card1;
    [SerializeField]
    private Image card2;

    public void SetCards(List<Card> cards, int percentTowardNextCard) {
        // assume always [0,2] cards
        if (cards.Count == 0) {
            card1.enabled = false;
            card2.enabled = false;
            HandlePercent(percentTowardNextCard);
        } else if (cards.Count == 1) {
            BindCard(card1, cards[0]);
            HandlePercent(percentTowardNextCard);
        } else if (cards.Count == 2) {
            BindCard(card1, cards[0]);
            BindCard(card2, cards[1]);
        }
    }

    private void BindCard(Image image, Card card) {
        if (card != null) {
            image.sprite = card.cardArt;
            image.enabled = true;
        } else {
            image.enabled = false;
        }
    }

    private void HandlePercent(int percentTowardNextCard) {
        
    }
}
