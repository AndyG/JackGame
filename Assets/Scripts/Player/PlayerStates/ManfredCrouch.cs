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
  }

  public override void Exit()
  {
    Debug.Log("Exit Crouch");
  }

  public override void Update()
  {
    manfred.playerInput.GatherInput();

    // Player is no longer holding crouch.
    if (manfred.playerInput.GetVerticalInput() >= 0f)
    {
      Debug.Log("input: " + manfred.playerInput.GetVerticalInput());
      this.fsm.ChangeState(manfred.stateIdle);
    }
  }

  public override string GetAnimation()
  {
    return "ManfredCrouch";
  }
}
