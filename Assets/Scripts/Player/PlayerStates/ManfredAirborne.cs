using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredAirborne : FSM2.State
{

  private Manfred manfred;

  private float airborneFloatingThreshold = 10;

  public ManfredAirborne(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Update()
  {
    // add gravity
    manfred.velocity.y += manfred.gravity * Time.deltaTime;

    // move
    manfred.controller.Move(manfred.velocity * Time.deltaTime);

    // check if hit ground
    if (manfred.controller.GetCollisions().below)
    {
      manfred.velocity.y = 0;
      this.fsm.ChangeState(manfred.stateIdle);
    }
  }

  public override string GetAnimation()
  {
    if (manfred.velocity.y > airborneFloatingThreshold)
    {
      // check state and determine which frame to play.
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
