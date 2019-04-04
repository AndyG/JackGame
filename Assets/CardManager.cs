using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

  System.Random random = new System.Random();

  [SerializeField]
  private List<Card> cards;

  [SerializeField]
  private int percentTowardNextCard = 0;

  [SerializeField]
  private CardTypeMapper cardTypeMapper;

  private UIManager uiManager;

  void Start()
  {
    uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
  }

  void OnEnable()
  {
    NotifyUIManagerCards();
  }

  void OnValidate()
  {
    NotifyUIManagerCards();
  }

  public void AddPercentToCard(int percent)
  {
    this.percentTowardNextCard += percent;
    if (this.percentTowardNextCard >= 100)
    {
      Card card = GenerateNextCard();
      this.cards.Add(card);
      this.percentTowardNextCard = 0;
    }
    NotifyUIManagerCards();
  }

  private Card GenerateNextCard()
  {
    CardType[] cardTypes = (CardType[])Enum.GetValues(typeof(CardType));
    int randomCardIndex = random.Next(cardTypes.Length);
    CardType cardType = cardTypes[randomCardIndex];
    Card card = cardTypeMapper.GetCard(cardType);
    return card;
  }

  private void NotifyUIManagerCards()
  {
    if (uiManager != null)
    {
      uiManager.SetCards(cards, percentTowardNextCard);
    }
  }
}
