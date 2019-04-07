﻿using System;
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
    NotifyUIManagerCards();
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
    if (this.cards.Count == 2)
    {
      return;
    }

    this.percentTowardNextCard += percent;
    if (this.percentTowardNextCard >= 100)
    {
      Card card = GenerateNextCard();
      this.cards.Add(card);
      if (this.cards.Count < 2)
      {
        this.percentTowardNextCard = this.percentTowardNextCard - 100; // assumes you never got 200+ percent at once
      }
      else
      {
        this.percentTowardNextCard = 0;
      }
    }
    NotifyUIManagerCards();
  }

  public CardType? ConsumeCard()
  {
    if (this.cards.Count > 0)
    {
      Card card = this.cards[0];
      this.cards.RemoveAt(0);
      NotifyUIManagerCards();
      return card.cardType;
    }

    return null;
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
