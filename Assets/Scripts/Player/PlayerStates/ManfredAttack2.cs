using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredAttack2 : FSM2.State
{

  private Manfred manfred;
  private bool allowNextAttack = false;
  private bool lockout = false;

  public ManfredAttack2(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Enter()
  {
    allowNextAttack = false;
    lockout = false;
  }

  public override void Update()
  {
    if (manfred.playerInput.GetDidPressAttack())
    {
      if (!allowNextAttack)
      {
        lockout = true;
      }
      else if (!lockout)
      {
        this.fsm.ChangeState(manfred.stateAttack3);
      }
    }
  }

  public override void OnMessage(string message)
  {
    if (message.Equals("EndAttack2"))
    {
      this.fsm.ChangeState(manfred.stateGrounded);
      return;
    }

    if (message.Equals("StartWindow"))
    {
      allowNextAttack = true;
    }
  }

  public override string GetAnimation()
  {
    return "ManfredAttack2";
  }
}
