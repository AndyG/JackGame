using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[CreateAssetMenu(fileName = "Input", menuName = "PlayerInput", order = 1)]
public class PlayerInput : ScriptableObject
{

  [SerializeField]
  private int playerId;

  [SerializeField]
  private float horizontalDeadzone = 0.02f;

  [SerializeField]
  private float verticalDeadzone = 0.02f;

  [SerializeField]
  private float jumpBufferTime;

  private Player player;

  private float horizInput;
  private float verticalInput;
  private bool didPressJump;
  private bool didReleaseJump;
  private bool didPressAttack;
  private bool didPressParry;

  private ActionBuffer actionBuffer;

  public void Awake()
  {
    player = ReInput.players.GetPlayer(playerId);
    actionBuffer = new ActionBuffer();
  }

  public void Update()
  {
    actionBuffer.Update();
    Clear();
    horizInput = _GetHorizInput();
    verticalInput = _GetVerticalInput();
    didPressJump = _GetDidPressJump();
    didReleaseJump = _GetDidReleaseJump();
    didPressAttack = _GetDidPressAttack();
    didPressParry = _GetDidPressParry();

    if (didPressJump)
    {
      actionBuffer.Buffer(ACTION_JUMP, jumpBufferTime);
    }
    else if (didReleaseJump)
    {
      actionBuffer.ClearAction(ACTION_JUMP);
    }
  }

  private void Clear()
  {
    horizInput = 0f;
    didPressJump = false;
    didReleaseJump = false;
    didPressAttack = false;
  }

  #region Getters
  public float GetHorizInput() => horizInput;
  public float GetVerticalInput() => verticalInput;
  public bool GetDidPressJump() => didPressJump;
  public bool GetDidPressJumpBuffered() => actionBuffer.ConsumeBuffer(ACTION_JUMP);
  public bool GetDidReleaseJump() => didReleaseJump;
  public bool GetDidPressAttack() => didPressAttack;
  public bool GetDidPressParry() => didPressParry;
  #endregion

  #region Get raw input

  private float _GetHorizInput()
  {
    float h = player.GetAxis("MoveHorizontal");
    if (Mathf.Abs(h) < horizontalDeadzone)
    {
      return 0;
    }
    else
    {
      return Mathf.Sign(h);
    }
  }

  private float _GetVerticalInput()
  {
    float v = Input.GetAxis("Vertical");
    // player.GetAxis("MoveVertical");
    if (Mathf.Abs(v) < verticalDeadzone)
    {
      return 0;
    }
    else
    {
      return Mathf.Sign(v);
    }
  }

  private bool _GetDidPressJump() => player.GetButtonDown("Jump");
  private bool _GetDidReleaseJump() => player.GetButtonUp("Jump");
  private bool _GetDidPressAttack() => player.GetButtonDown("PrimaryAttack");
  private bool _GetDidPressParry() => player.GetButtonDown("Parry");
  #endregion

  #region Buffer keys
  private static string ACTION_JUMP = "Jump";
  #endregion
}
