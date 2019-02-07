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

  public override void Update()
  {
    manfred.playerInput.GatherInput();

    if (manfred.playerInput.GetDidPressJump())
    {
      manfred.velocity = manfred.groundedJumpPower * Vector2.up;
      this.fsm.ChangeState(manfred.stateAirborne);
      return;
    }

    // Player is no longer holding crouch.
    if (manfred.playerInput.GetVerticalInput() >= 0f)
    {
      this.fsm.ChangeState(manfred.stateGrounded);
    }
  }

  public override string GetAnimation()
  {
    return "ManfredCrouch";
  }
}
