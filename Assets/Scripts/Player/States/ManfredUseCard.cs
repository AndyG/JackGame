using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredUseCard : ManfredStates.ManfredState0Param
{

  private bool isOnGround = false;
  private Cinemachine.CinemachineImpulseSource deathImpulseSource;

  void Start() {
    this.deathImpulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();
  }

  public override void Enter() {
  }

  public override void Tick() {
  }

  public override string GetAnimation()
  {
    return "VampUseCard";
  }
   
  public override void OnMessage(string message) {
    if (message == "OnUseCardEnded") {
      TransitionAway();
    } else if (message == "ConsumeCard") {
      ConsumeCard();
    }
  }

  private void TransitionAway() {
      manfred.fsm.ChangeState(manfred.stateAirborne, manfred.stateAirborne);
  }

  private void ConsumeCard() {
    CardType? cardType = manfred.cardManager.ConsumeCard();
    if (cardType.HasValue) {
      UseCard(cardType.Value);
    }
  }

  private void UseCard(CardType cardType) {
    switch(cardType) {
      case CardType.DEATH:
        UseDeath();
        break;
      case CardType.LOVER:
        UseLover();
        break;
    }
  }

  private void UseDeath() {
    deathImpulseSource.GenerateImpulse();
    manfred.effectsCanvas.DoWhiteFlash(0.5f);

    TrashEnemy[] enemies = (TrashEnemy[]) Object.FindObjectsOfType(typeof(TrashEnemy));
    for (int i = 0; i < enemies.Length; i++) {
      enemies[i].OnDeathUsed();
    }
  }

  private void UseLover() {

  }
}
