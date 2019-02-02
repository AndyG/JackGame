using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredCrouch : FSM2.State
{

  private Manfred manfred;

  public ManfredCrouch(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Enter()
  {
    Debug.Log("Enter Crouch");
    manfred.animator.SetBool("IsCrouching", true);
  }

  // Update is called once per frame
  public override void Update()
  {
    if (Input.GetKeyUp(KeyCode.P))
    {
      this.fsm.ChangeState(manfred.stateIdle);
    }
  }
}
