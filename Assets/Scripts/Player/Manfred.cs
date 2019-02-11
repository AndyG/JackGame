using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Manfred : MonoBehaviour, AnimationManager.AnimationProvider, Hurtable
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
  public float velocityXSmoothFactorGrounded = 0.2f;
  public float velocityXSmoothFactorAirborne = 0.4f;

  public bool isFacingRight = true;

  // smoothing function
  public float velocityXSmoothing = 0f;

  private FSM2 fsm;

  public FSM2.State stateAttack1;
  public FSM2.State stateAttack2;
  public FSM2.State stateAttack3;
  public FSM2.State stateGrounded;
  public FSM2.State stateCrouch;
  public FSM2.State stateAirborne;
  public FSM2.State stateParryStance;
  public ManfredParryAction stateParryAction;

  void Awake()
  {
    playerInput.Awake();
  }

  void Start()
  {
    animator = GetComponent<Animator>();
    controller = GetComponent<PlayerController>();

    animationManager = new AnimationManager(animator, this);

    fsm = new FSM2();
    stateAttack1 = new ManfredAttack1(this);
    stateAttack2 = new ManfredAttack2(this);
    stateAttack3 = new ManfredAttack3(this);
    stateGrounded = new ManfredGrounded(this);
    stateCrouch = new ManfredCrouch(this);
    stateAirborne = new ManfredAirborne(this);
    stateParryStance = new ManfredParryStance(this);
    stateParryAction = new ManfredParryAction(this);

    fsm.ChangeState(stateAirborne);
  }

  void Update()
  {
    playerInput.Update();
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

  public HurtInfo OnHit(HitInfo hitInfo)
  {
    TimeManagerSingleton.Instance.DoDramaticPause(0.2f);

    if (fsm.currentState != null)
    {
      HurtInfo hurtInfo = fsm.currentState.OnHit(hitInfo);
      if (hurtInfo != null)
      {
        return hurtInfo;
      }
    }

    return new HurtInfo(true);
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
