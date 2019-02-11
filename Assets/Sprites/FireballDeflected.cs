using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDeflected : MonoBehaviour
{

  [SerializeField]
  private float velocity;

  void Update()
  {
    transform.Translate(-transform.right * velocity * Time.deltaTime, Space.World);
  }
}
