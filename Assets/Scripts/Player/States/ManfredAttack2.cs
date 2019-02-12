using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManfredAttack2", menuName = "ManfredStates/ManfredAttack2")]
public class ManfredAttack2 : ManfredStates.ManfredState0Param
{

  private bool allowNextAttack = false;
  private bool lockout = false;

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
        manfred.fsm.ChangeState(manfred.stateAttack3, manfred.stateAttack3);
      }
    }
  }

  public override void OnMessage(string message)
  {
    if (message.Equals("EndAttack2"))
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
    return "ManfredAttack2";
  }
}
