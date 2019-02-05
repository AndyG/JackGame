using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerInput : MonoBehaviour
{

  [SerializeField]
  private int playerId;

  [SerializeField]
  private float horizontalDeadzone = 0.02f;

  [SerializeField]
  private float verticalDeadzone = 0.02f;

  private Player player;

  private float horizInput;
  private float verticalInput;
  private bool didPressJump;
  private bool didReleaseJump;
  private bool didPressAttack;
  private bool didPressGrapple;

  void Awake()
  {
    player = ReInput.players.GetPlayer(playerId);
  }

  public void GatherInput()
  {
    Clear();
    horizInput = _GetHorizInput();
    verticalInput = _GetVerticalInput();
    didPressJump = _GetDidPressJump();
    didReleaseJump = _GetDidReleaseJump();
    didPressAttack = _GetDidPressAttack();
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
  public bool GetDidReleaseJump() => didReleaseJump;
  public bool GetDidPressAttack() => didPressAttack;
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
  #endregion
}
