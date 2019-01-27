using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

  [SerializeField]
  private float velocity;

  void Start()
  {

  }

  void Update()
  {
    transform.Translate(transform.right * -1 * velocity * Time.deltaTime);
  }
}
