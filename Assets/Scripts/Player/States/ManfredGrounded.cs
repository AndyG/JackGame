﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredGrounded : ManfredStates.ManfredState0Param
{

  [SerializeField]
  private float jumpPower;

  private float attackCooldown = 0.05f;
  private float timeInState = 0.25f;

  public override void Enter()
  {
    timeInState = 0f;
  }

  public override void Tick()
  {
    timeInState += Time.deltaTime;

    if (manfred.playerInput.GetDidPressJumpBuffered())
    {
      manfred.velocity.y = jumpPower;
      manfred.fsm.ChangeState(manfred.stateAirborne, manfred.stateAirborne);
      return;
    }

    if (manfred.playerInput.GetHorizInput() == 0f && manfred.playerInput.GetVerticalInput() < 0)
    {
      manfred.velocity.x = 0;
      manfred.fsm.ChangeState(manfred.stateCrouch, manfred.stateCrouch);
      return;
    }

    if (timeInState >= attackCooldown && manfred.playerInput.GetDidPressAttack())
    {
      manfred.fsm.ChangeState(manfred.stateAttack1, manfred.stateAttack1);
      return;
    }

    if (manfred.playerInput.GetDidPressParry())
    {
      manfred.fsm.ChangeState(manfred.stateParryStance, manfred.stateParryStance);
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
      manfred.fsm.ChangeState(manfred.stateAirborne, manfred.stateAirborne);
    }
  }

  public override string GetAnimation()
  {
    return "ManfredIdle";
  }
}