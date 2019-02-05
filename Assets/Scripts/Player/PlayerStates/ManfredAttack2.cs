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
    Debug.Log("Enter Attack2");
    allowNextAttack = false;
    lockout = false;
  }

  public override void Exit()
  {
    Debug.Log("Exit Attack2");
  }

  public override void Update()
  {
    manfred.playerInput.GatherInput();

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
      this.fsm.ChangeState(manfred.stateIdle);
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
