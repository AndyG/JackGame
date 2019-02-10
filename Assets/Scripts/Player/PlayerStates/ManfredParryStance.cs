using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredParryStance : FSM2.State
{

  private Manfred manfred;

  public ManfredParryStance(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Update()
  {
  }

  public override string GetAnimation()
  {
    return "ManfredParryStance";
  }
}
