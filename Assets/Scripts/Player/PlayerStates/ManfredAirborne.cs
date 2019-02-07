using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredAirborne : FSM2.State
{

  private Manfred manfred;

  private float airborneFloatingThreshold = 7;

  public ManfredAirborne(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Update()
  {
    manfred.playerInput.GatherInput();

    if (manfred.velocity.y > manfred.minJumpVelocity && manfred.playerInput.GetDidReleaseJump())
    {
      manfred.velocity.y = manfred.minJumpVelocity;
    }

    // move
    float horizInput = manfred.playerInput.GetHorizInput();
    manfred.velocity.x = horizInput * manfred.horizSpeed;
    manfred.velocity.y += manfred.gravity * Time.deltaTime;

    if (horizInput != 0f)
    {
      manfred.FaceMovementDirection();
    }

    manfred.controller.Move(manfred.velocity * Time.deltaTime);

    // check if hit ground
    if (manfred.controller.GetCollisions().below)
    {
      manfred.velocity.y = 0f;
      this.fsm.ChangeState(manfred.stateGrounded);
      return;
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
