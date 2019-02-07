using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredGrounded : FSM2.State
{

  private Manfred manfred;

  private float attackCooldown = 0.05f;
  private float timeInState = 0.25f;

  public ManfredGrounded(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Enter()
  {
    timeInState = 0f;
  }

  public override void Update()
  {
    timeInState += Time.deltaTime;

    PlayerController.CollisionInfo collisionInfo = manfred.controller.GetCollisions();
    manfred.playerInput.GatherInput();

    if (!collisionInfo.below)
    {
      this.fsm.ChangeState(manfred.stateAirborne);
      return;
    }

    if (manfred.playerInput.GetDidPressJump())
    {
      manfred.velocity.y = manfred.groundedJumpPower;
      this.fsm.ChangeState(manfred.stateAirborne);
      return;
    }

    if (manfred.playerInput.GetHorizInput() == 0f && manfred.playerInput.GetVerticalInput() < 0)
    {
      this.fsm.ChangeState(manfred.stateCrouch);
      return;
    }

    if (timeInState >= attackCooldown && manfred.playerInput.GetDidPressAttack())
    {
      this.fsm.ChangeState(manfred.stateAttack1);
      return;
    }

    float horizInput = manfred.playerInput.GetHorizInput();
    manfred.velocity.x = horizInput * manfred.horizSpeed;

    manfred.velocity.y = manfred.gravity * Time.deltaTime;

    manfred.controller.Move(manfred.velocity * Time.deltaTime);
  }

  public override string GetAnimation()
  {
    return "ManfredIdle";
  }
}
