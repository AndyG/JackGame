using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredAttack1 : FSM2.State
{

  private Manfred manfred;
  private bool allowNextAttack = false;
  private bool lockout = false;

  public ManfredAttack1(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Enter()
  {
    Debug.Log("Enter Attack1");
    allowNextAttack = false;
    lockout = false;
    manfred.animator.SetBool("Attack1", true);
  }

  public override void Exit()
  {
    Debug.Log("Exit Attack1");
    manfred.animator.SetBool("Attack1", false);
  }

  public override void Update()
  {
    if (Input.GetKeyDown(KeyCode.S))
    {
      if (!allowNextAttack)
      {
        lockout = true;
      }
      else if (!lockout)
      {
        this.fsm.ChangeState(manfred.stateAttack2);
      }
      return;
    }

    if (Input.GetKeyDown(KeyCode.T))
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
    if (message.Equals("EndAttack1"))
    {
      this.fsm.ChangeState(manfred.stateIdle);
      return;
    }

    if (message.Equals("StartWindow"))
    {
      allowNextAttack = true;
    }
  }
}
