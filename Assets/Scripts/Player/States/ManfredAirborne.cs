using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredAirborne : ManfredStates.ManfredState0Param
{

  private float airborneFloatingThreshold = 7;

  public override void Tick()
  {
    if (manfred.velocity.y > manfred.minJumpVelocity && manfred.playerInput.GetDidReleaseJump())
    {
      manfred.velocity.y = manfred.minJumpVelocity;
    }

    // move
    float horizInput = manfred.playerInput.GetHorizInput();
    float targetVelocityX = horizInput * manfred.horizSpeed;
    manfred.velocity.x = Mathf.SmoothDamp(
      manfred.velocity.x,
      targetVelocityX,
      ref manfred.velocityXSmoothing,
      manfred.velocityXSmoothFactorAirborne);

    manfred.velocity.y += manfred.gravity * Time.deltaTime;

    if (horizInput != 0f)
    {
      manfred.FaceMovementDirection();
    }

    manfred.controller.Move(manfred.velocity * Time.deltaTime);

    if (manfred.playerInput.GetDidPressParry()) {
      manfred.fsm.ChangeState(manfred.stateUseCard, manfred.stateUseCard);
      return;
    }

    if (manfred.playerInput.GetDidPressSiphon()) {
      manfred.fsm.ChangeState(manfred.stateSiphonStart, manfred.stateSiphonStart);
      return;
    }

    // check if hit ground
    if (manfred.controller.GetCollisions().below)
    {
      manfred.velocity.y = 0f;
      manfred.fsm.ChangeState(manfred.stateGrounded, manfred.stateGrounded);
    }
  }

  public override string GetAnimation()
  {
    if (manfred.velocity.y > airborneFloatingThreshold)
    {
      return "ManfredRising";
    }
    else if (manfred.velocity.y < -airborneFloatingThreshold)
    {
      return "ManfredFalling";
    }
    else
    {
      return "ManfredAirborne";
    }
  }
}
