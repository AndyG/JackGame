using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Manfred : MonoBehaviour
{

  private State state;
  private Animator animator;
  private FSM2 fsm;

  [SerializeField]
  private float parryDuration;

  private float parryTime = 0f;

  private bool ignoreAnimationEventsThisFrame;

  void Start()
  {
    this.animator = GetComponent<Animator>();
    this.state = State.STANDING;
  }

  void Update()
  {
    ignoreAnimationEventsThisFrame = false;
    switch (state)
    {
      case State.STANDING:
        UpdateStanding();
        break;
      case State.PARRY_START:
        UpdateParryStart();
        break;
      case State.PARRY_ACTIVE:
        UpdateParryActive();
        break;
      default:
        Debug.Log("no handler for state: " + state);
        break;
    }
  }

  public void OnIdle()
  {
    SetState(State.STANDING);
    animator.SetTrigger("Idle");
  }

  public void OnParryActive()
  {
    SetState(State.PARRY_ACTIVE);
  }

  private void UpdateStanding()
  {
    if (Input.GetKeyDown(KeyCode.P))
    {
      SetState(State.PARRY_START);
      animator.SetTrigger("ParryStart");
      ignoreAnimationEventsThisFrame = true;
    }
  }

  private void UpdateParryStart()
  {
    parryTime = 0f;
    //no-op
  }

  private void UpdateParryActive()
  {
    parryTime += Time.deltaTime;
    if (parryTime >= parryDuration || !Input.GetKey(KeyCode.P))
    {
      SetState(State.PARRY_RETRACT);
      animator.SetTrigger("ParryRetract");
      return;
    }
  }

  private void SetState(State state)
  {
    Debug.Log("SetState: " + state);
    this.state = state;
  }

  private enum State
  {
    STANDING,
    PARRY_START,
    PARRY_ACTIVE,
    PARRY_RETRACT,
    PARRY_CATCH
  }
}
