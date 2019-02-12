using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManfredParryAction", menuName = "ManfredStates/ManfredParryAction")]
public class ManfredParryAction : ManfredStates.ManfredState2Params<GameObject, Vector3>
{

  private float duration = 0.3f;

  private float timeInState = 0f;

  private GameObject spawnObject;
  private Vector3 parryPosition;
  private bool hasSpawnedObject = false;

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
      manfred.fsm.ChangeState(manfred.stateGrounded, manfred.stateGrounded);
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
