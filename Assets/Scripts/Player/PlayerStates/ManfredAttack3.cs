using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredAttack3 : FSM2.State
{

  private Manfred manfred;

  public ManfredAttack3(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void OnMessage(string message)
  {
    if (message.Equals("EndAttack3"))
    {
      this.fsm.ChangeState(manfred.stateGrounded);
      return;
    }
  }

  public override string GetAnimation()
  {
    return "ManfredAttack3";
  }
}
