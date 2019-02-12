using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManfredCrouch", menuName = "ManfredStates/ManfredCrouch")]
public class ManfredCrouch : ManfredStates.ManfredState0Param
{

  [SerializeField]
  private float jumpPower;

  public override void Update()
  {
    if (manfred.playerInput.GetDidPressJumpBuffered())
    {
      manfred.velocity = jumpPower * Vector2.up;
      manfred.fsm.ChangeState(manfred.stateAirborne, manfred.stateAirborne);
      return;
    }

    // Manfred is no longer holding crouch.
    if (manfred.playerInput.GetVerticalInput() >= 0f)
    {
      manfred.fsm.ChangeState(manfred.stateGrounded, manfred.stateGrounded);
    }
  }

  public override string GetAnimation()
  {
    return "ManfredCrouch";
  }
}
