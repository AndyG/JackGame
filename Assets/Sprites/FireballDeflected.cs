using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class FireballDeflected : MonoBehaviour, Directable
{

  [SerializeField]
  private float speed;

  void Update()
  {
    transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
  }

  public void DirectToward(Vector2 direction) {
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }

  void OnTriggerEnter2D(Collider2D other) {
    Target target = other.transform.gameObject.GetComponent<Target>();
    if (target != null) {
      target.Explode();
      Destroy(this.transform.gameObject);
    }
  }
}
