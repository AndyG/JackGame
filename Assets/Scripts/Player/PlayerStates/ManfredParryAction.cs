using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredParryAction : FSM2.State2Params<GameObject, Vector3>
{

  private float duration = 0.3f;

  private Manfred manfred;

  private float timeInState = 0f;

  private GameObject spawnObject;
  private Vector3 parryPosition;
  private bool hasSpawnedObject = false;

  public ManfredParryAction(Manfred manfred)
  {
    this.manfred = manfred;
  }

  public override void Enter(GameObject spawnObject, Vector3 parryPosition)
  {
    timeInState = 0f;
    this.spawnObject = spawnObject;
    this.parryPosition = parryPosition;
    this.hasSpawnedObject = false;
  }

  public override void Update()
  {
    if (!hasSpawnedObject)
    {
      SpawnObject();
    }
    timeInState += Time.deltaTime;
    if (timeInState >= duration)
    {
      this.fsm.ChangeState(manfred.stateGrounded);
    }
  }

  public override string GetAnimation()
  {
    return "ManfredParryAction";
  }

  private void SpawnObject()
  {
    GameObject.Instantiate(spawnObject, parryPosition, Quaternion.AngleAxis(-30, Vector3.forward));
    hasSpawnedObject = true;
  }
}
