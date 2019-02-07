using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Manfred : MonoBehaviour, AnimationManager.AnimationProvider
{

  public Animator animator;
  public AnimationManager animationManager;
  public PlayerInput playerInput;
  public PlayerController controller;

  public float gravity;
  public Vector2 velocity = new Vector2(0f, 0f);
  public float horizSpeed = 0.5f;
  public float groundedJumpPower = 40;
  public float minJumpVelocity = 5;

  public bool isFacingRight = true;

  private FSM2 fsm;

  public FSM2.State stateAttack1;
  public FSM2.State stateAttack2;
  public FSM2.State stateAttack3;
  public FSM2.State stateGrounded;
  public FSM2.State stateCrouch;
  public FSM2.State stateAirborne;

  void Start()
  {
    animator = GetComponent<Animator>();
    playerInput = GetComponent<PlayerInput>();
    controller = GetComponent<PlayerController>();

    animationManager = new AnimationManager(animator, this);

    fsm = new FSM2();
    stateAttack1 = new ManfredAttack1(this);
    stateAttack2 = new ManfredAttack2(this);
    stateAttack3 = new ManfredAttack3(this);
    stateGrounded = new ManfredGrounded(this);
    stateCrouch = new ManfredCrouch(this);
    stateAirborne = new ManfredAirborne(this);

    fsm.ChangeState(stateAirborne);
  }

  void Update()
  {
    fsm.UpdateCurrentState();
    animationManager.Update();
    transform.localScale = new Vector3(IsFacingDefaultDirection() ? 1f : -1f, 1f, 1f);
  }

  public void OnMessage(string message)
  {
    if (fsm.currentState != null)
    {
      fsm.currentState.OnMessage(message);
    }
  }

  public string GetAnimation()
  {
    return fsm.currentState.GetAnimation();
  }

  public bool IsFacingDefaultDirection()
  {
    return isFacingRight;
  }

  public void FaceMovementDirection()
  {
    isFacingRight = velocity.x >= 0f;
  }
}
