using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredSiphonActive : ManfredStates.ManfredState1Param<SiphonSource>
{

  public override string GetAnimation()
  {
    return "ManfredParryStance";
  }

  private void TransitionToSiphonFailure()
  {

  }
}
