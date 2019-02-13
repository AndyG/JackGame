using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredAttack1 : ManfredStates.ManfredState0Param
{

  private bool allowNextAttack = false;
  private bool lockout = false;

  public override void Enter()
  {
    allowNextAttack = false;
    lockout = false;
  }

  public override void Tick()
  {
    if (manfred.playerInput.GetDidPressAttack())
    {
      if (!allowNextAttack)
      {
        lockout = true;
      }
      else if (!lockout)
      {
        manfred.fsm.ChangeState(manfred.stateAttack2, manfred.stateAttack2);
      }
    }
  }

  public override void OnMessage(string message)
  {
    if (message.Equals("EndAttack1"))
    {
      manfred.fsm.ChangeState(manfred.stateGrounded, manfred.stateGrounded);
      return;
    }

    if (message.Equals("StartWindow"))
    {
      allowNextAttack = true;
    }
  }

  public override string GetAnimation()
  {
    return "ManfredAttack1";
  }
}
