using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Manfred : MonoBehaviour, AnimationManager.AnimationProvider
{

  public Animator animator;
  public PlayerInput playerInput;
  public PlayerController controller;

  public float gravity;
  public Vector2 velocity = new Vector2(0f, 0f);
  public float horizSpeed = 0.5f;

  private FSM2 fsm;

  private bool ignoreAnimationEventsThisFrame;

  public FSM2.State stateAttack1;
  public FSM2.State stateAttack2;
  public FSM2.State stateAttack3;
  public FSM2.State stateIdle;
  public FSM2.State stateCrouch;

  void Start()
  {
    animator = GetComponent<Animator>();
    playerInput = GetComponent<PlayerInput>();
    controller = GetComponent<PlayerController>();

    fsm = new FSM2();

    stateAttack1 = new ManfredAttack1(this);
    stateAttack2 = new ManfredAttack2(this);
    stateAttack3 = new ManfredAttack3(this);
    stateIdle = new ManfredIdle(this);
    stateCrouch = new ManfredCrouch(this);
    fsm.ChangeState(stateIdle);
  }

  void Update()
  {
    ignoreAnimationEventsThisFrame = false;
    // update current state
    fsm.UpdateCurrentState();
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
}
