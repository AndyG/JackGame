using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Manfred : MonoBehaviour
{

  public Animator animator;
  private FSM2 fsm;

  [SerializeField]
  private float parryDuration;

  private float parryTime = 0f;

  private bool ignoreAnimationEventsThisFrame;

  public FSM2.State stateIdle;
  public FSM2.State stateCrouch;

  void Start()
  {
    animator = GetComponent<Animator>();
    fsm = new FSM2();
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
}
