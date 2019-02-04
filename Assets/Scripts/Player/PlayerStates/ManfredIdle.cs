using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredIdle : FSM2.State
{

  private Manfred manfred;

  private float attackCooldown = 0.05f;
  private float timeInState = 0.25f;

  public ManfredIdle(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Enter()
  {
    Debug.Log("Enter Idle");
    timeInState = 0f;
    manfred.animator.SetBool("Idle", true);
  }

  public override void Exit()
  {
    Debug.Log("Exit Idle");
    manfred.animator.SetBool("Idle", false);
  }

  public override void Update()
  {
    timeInState += Time.deltaTime;

    manfred.playerInput.GatherInput();

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
    if (horizInput != 0f)
    {
      manfred.velocity.x = horizInput * manfred.horizSpeed;
    }
    manfred.velocity.y += manfred.gravity * Time.deltaTime;

    manfred.controller.Move(manfred.velocity * Time.deltaTime);
  }
}
