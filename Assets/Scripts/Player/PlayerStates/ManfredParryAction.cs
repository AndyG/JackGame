using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredParryAction : FSM2.State
{

  private float duration = 0.3f;

  private Manfred manfred;

  private float timeInState = 0f;

  public ManfredParryAction(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Enter()
  {
    timeInState = 0f;
  }

  public override void Update()
  {
    timeInState += Time.deltaTime;
    if (timeInState >= duration)
    {
      this.fsm.ChangeState(manfred.stateGrounded);
    }
  }

  public override string GetAnimation()
  {
    return "ManfredParryAction";
  }
}
