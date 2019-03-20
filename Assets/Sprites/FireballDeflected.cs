using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDeflected : MonoBehaviour, Directable
{

  [SerializeField]
  private float speed;

  void Update()
  {
    transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
  }

  public void DirectToward(Vector2 direction) {
    Debug.Log("looking toward: " + direction);
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }
}
