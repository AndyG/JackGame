using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(HitboxManager))]
public class Manfred : MonoBehaviour, AnimationManager.AnimationProvider, Hurtable
{

  public Animator animator;
  public AnimationManager animationManager;
  public PlayerInput playerInput;
  public PlayerController controller;

  [System.NonSerialized]
  public CardManager cardManager;

  [System.NonSerialized]
  public HitboxManager hitboxManager;

  [System.NonSerialized]
  public SceneTransitioner sceneTransitioner;

  public float gravity;
  public Vector2 velocity = new Vector2(0f, 0f);
  public float horizSpeed = 0.5f;
  public float minJumpVelocity = 5;
  public float velocityXSmoothFactorGrounded = 0.2f;
  public float velocityXSmoothFactorAirborne = 0.4f;

  public bool isFacingRight = true;

  // smoothing function
  public float velocityXSmoothing = 0f;

  public Transform siphonSinkTransform;

  public FSM<Manfred, ManfredStates.IManfredState> fsm;

  [Header("States")]
  public ManfredAttack1 stateAttack1;
  public ManfredAttack2 stateAttack2;
  public ManfredAttack3 stateAttack3;
  public ManfredGrounded stateGrounded;
  public ManfredCrouch stateCrouch;
  public ManfredAirborne stateAirborne;
  public ManfredParryStance stateParryStance;
  public ManfredParryAction stateParryAction;
  public ManfredSiphonStart stateSiphonStart;
  public ManfredSiphonRecovery stateSiphonRecovery;
  public ManfredSiphonActive stateSiphonActive;
  public ManfredDead stateDead;

  void Awake()
  {
    playerInput.Awake();
  }

  void Start()
  {
    animator = GetComponent<Animator>();
    controller = GetComponent<PlayerController>();
    hitboxManager = GetComponent<HitboxManager>();
    sceneTransitioner = (SceneTransitioner) FindObjectOfType(typeof(SceneTransitioner));

    cardManager = (CardManager)FindObjectOfType(typeof(CardManager));

    animationManager = new AnimationManager(animator, this);

    fsm = new FSM<Manfred, ManfredStates.IManfredState>(this);

    fsm.ChangeState(stateAirborne, stateAirborne);
  }

  void Update()
  {
    playerInput.Update();
    fsm.TickCurrentState();
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
    HurtInfo hurtInfo = fsm.currentState.OnHit(hitInfo);
    if (hurtInfo.hitConnected) {
      fsm.ChangeState(stateDead, stateDead);
      StartCoroutine(DelayedRestartScene());
    }

    return hurtInfo;
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

  private IEnumerator DelayedRestartScene() {
    yield return new WaitForSeconds(3f);
    sceneTransitioner.RestartScene();
  }
}
