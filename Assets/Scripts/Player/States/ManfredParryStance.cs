using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredParryStance : ManfredStates.ManfredState0Param
{

  public override HurtInfo OnHit(HitInfo hitInfo)
  {
    manfred.fsm.ChangeState(manfred.stateParryAction, manfred.stateParryAction, hitInfo.parrySpawnObjectPrototype, hitInfo.position);
    return new HurtInfo(true);
  }

  public override string GetAnimation()
  {
    return "ManfredParryStance";
  }

  public override bool IsFacingDefaultDirection()
  {
    return true;
  }
}
