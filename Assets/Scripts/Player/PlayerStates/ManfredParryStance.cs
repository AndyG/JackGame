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

  public override HurtInfo OnHit(HitInfo hitInfo)
  {
    this.fsm.ChangeState(manfred.stateParryAction);
    return new HurtInfo(false);
  }

  public override string GetAnimation()
  {
    return "ManfredParryStance";
  }
}
