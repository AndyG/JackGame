using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManfredAttack3", menuName = "ManfredStates/ManfredAttack3")]
public class ManfredAttack3 : ManfredStates.ManfredState0Param
{

  public override void OnMessage(string message)
  {
    if (message.Equals("EndAttack3"))
    {
      manfred.fsm.ChangeState(manfred.stateGrounded, manfred.stateGrounded);
    }
  }

  public override string GetAnimation()
  {
    return "ManfredAttack3";
  }
}
