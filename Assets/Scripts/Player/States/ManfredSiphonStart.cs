using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredSiphonStart : ManfredStates.ManfredState0Param
{

  public override void OnMessage(string message)
  {
    if (message.Equals("DoSiphonSourceCheck"))
    {
      DoSiphonSourceCheck();
    }
  }

  public override string GetAnimation()
  {
    return "ManfredSiphonStart";
  }

  private void DoSiphonSourceCheck()
  {
    SiphonSource siphonSource = FindNearestSiphonSource();
    if (siphonSource != null)
    {
      TransitionToSiphonActive(siphonSource);
    }
    else
    {
      TransitionToSiphonRecovery();
    }
  }

  private void TransitionToSiphonActive(SiphonSource siphonSource)
  {

  }

  private void TransitionToSiphonRecovery()
  {
    manfred.fsm.ChangeState(manfred.stateSiphonRecovery, manfred.stateSiphonRecovery);
  }

  private SiphonSource FindNearestSiphonSource()
  {
    return null;
  }
}
