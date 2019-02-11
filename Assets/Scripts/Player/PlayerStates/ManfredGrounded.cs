﻿using System.Collections;
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

    if (manfred.playerInput.GetDidPressJumpBuffered())
    {
      manfred.velocity.y = manfred.groundedJumpPower;
      this.fsm.ChangeState(manfred.stateAirborne);
      return;
    }

    if (manfred.playerInput.GetHorizInput() == 0f && manfred.playerInput.GetVerticalInput() < 0)
    {
      manfred.velocity.x = 0;
      this.fsm.ChangeState(manfred.stateCrouch);
      return;
    }

    if (timeInState >= attackCooldown && manfred.playerInput.GetDidPressAttack())
    {
      this.fsm.ChangeState(manfred.stateAttack1);
      return;
    }

    if (manfred.playerInput.GetDidPressParry())
    {
      this.fsm.ChangeState(manfred.stateParryStance);
      return;
    }

    float horizInput = manfred.playerInput.GetHorizInput();

    float targetVelocityX = horizInput * manfred.horizSpeed;
    manfred.velocity.x = Mathf.SmoothDamp(
      manfred.velocity.x,
      targetVelocityX,
      ref manfred.velocityXSmoothing,
      manfred.velocityXSmoothFactorGrounded);

    // manfred.velocity.y = Mathf.Min(-0.1f, manfred.gravity * Time.deltaTime);
    // Debug.Log("computed y velocity: " + manfred.velocity.y);
    manfred.velocity.y = manfred.gravity * Time.deltaTime;

    if (horizInput != 0f)
    {
      manfred.FaceMovementDirection();
    }
    manfred.controller.Move(manfred.velocity * Time.deltaTime);

    PlayerController.CollisionInfo collisionInfo = manfred.controller.GetCollisions();
    if (!collisionInfo.below)
    {
      this.fsm.ChangeState(manfred.stateAirborne);
      return;
    }
  }

  public override string GetAnimation()
  {
    return "ManfredIdle";
  }
}
