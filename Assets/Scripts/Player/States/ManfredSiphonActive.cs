using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredSiphonActive : ManfredStates.ManfredState0Param
{

  public override void Tick()
  {
    if (!manfred.playerInput.GetIsHoldingSiphon())
    {
      TransitionToSiphonRecovery();
      return;
    }

    manfred.cardManager.AddPercentToCard(1);
  }

  public override string GetAnimation()
  {
    return "ManfredSiphonActive";
  }

  private void TransitionToSiphonRecovery()
  {
    manfred.fsm.ChangeState(manfred.stateSiphonRecovery, manfred.stateSiphonRecovery);
  }
}
