using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 Can spawn an object.
 */
public class Spawner : MonoBehaviour
{

  [SerializeField]
  private GameObject spawnObject;

  public void Spawn()
  {
    GameObject newObj = GameObject.Instantiate(this.spawnObject, this.transform.position, Quaternion.identity);
  }
}
