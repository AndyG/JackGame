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

  public override void Enter()
  {
    Debug.Log("Enter Attack3");
    manfred.animator.SetBool("Attack3", true);
  }

  public override void Exit()
  {
    Debug.Log("Exit Attack3");
    manfred.animator.SetBool("Attack3", false);
  }

  public override void Update()
  {
  }

  public override void OnMessage(string message)
  {
    if (message.Equals("EndAttack3"))
    {
      this.fsm.ChangeState(manfred.stateIdle);
      return;
    }
  }
}
