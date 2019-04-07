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

    float horizInput = manfred.playerInput.GetHorizInput();
    if (manfred.lockAirborneMovementTime <= 0f)
    {
      // move
      float targetVelocityX = horizInput * manfred.horizSpeed;
      manfred.velocity.x = Mathf.SmoothDamp(
        manfred.velocity.x,
        targetVelocityX,
        ref manfred.velocityXSmoothing,
        manfred.velocityXSmoothFactorAirborne);

    }

    if (manfred.velocity.x != 0f)
    {
      manfred.FaceMovementDirection();
    }

    manfred.velocity.y += manfred.gravity * Time.deltaTime;
    manfred.controller.Move(manfred.velocity * Time.deltaTime);

    if (manfred.playerInput.GetDidPressParry())
    {
      manfred.fsm.ChangeState(manfred.stateUseCard, manfred.stateUseCard);
      return;
    }

    if (manfred.playerInput.GetDidPressSiphon())
    {
      manfred.fsm.ChangeState(manfred.stateSiphonStart, manfred.stateSiphonStart);
      return;
    }

    // check if hit ground
    PlayerController.CollisionInfo collisions = manfred.controller.GetCollisions();
    if (collisions.below)
    {
      manfred.velocity.y = 0f;
      manfred.fsm.ChangeState(manfred.stateGrounded, manfred.stateGrounded);
      return;
    }

    // check if hit ground
    if (collisions.left && horizInput < 0 || collisions.right && horizInput > 0)
    {
      manfred.velocity.x = 0f;
      manfred.velocity.y = 0f;
      bool isWallOnLeft = collisions.left;
      manfred.fsm.ChangeState(manfred.stateWallCling, manfred.stateWallCling, isWallOnLeft);
      return;
    }
  }

  public override string GetAnimation()
  {
    if (manfred.velocity.y > 0)
    {
      return "VampAirborneUp";
    }
    else
    {
      return "VampAirborneDown";
    }
  }
}
