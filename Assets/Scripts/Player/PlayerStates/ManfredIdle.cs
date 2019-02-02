using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredIdle : FSM2.State
{

  private Manfred manfred;

  public ManfredIdle(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Enter()
  {
    Debug.Log("Enter Idle");
    manfred.animator.SetBool("IsCrouching", false);
  }

  // Update is called once per frame
  public override void Update()
  {
    if (Input.GetKeyDown(KeyCode.P))
    {
      this.fsm.ChangeState(manfred.stateCrouch);
    }
  }
}
