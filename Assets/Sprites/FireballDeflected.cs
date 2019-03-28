using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class FireballDeflected : MonoBehaviour, Directable
{

  [SerializeField]
  private float speed;

  private Vector2 direction;

  void Start() {
    if (direction == null) {
      direction = transform.right;
    }
  }

  void Update()
  {
    transform.Translate(direction * speed * Time.deltaTime, Space.World);
  }

  public void DirectToward(Vector2 direction) {
    this.direction = direction;
  }

  void OnColliderEnter2D(Collider2D other) {
    bool handleHit = DidHitTarget(other) || DidHitMirror(other);
  }

  void OnCollisionEnter2D() {
    Poof();
  }

  void OnTriggerEnter2D(Collider2D other) {
    bool handleHit = DidHitTarget(other) || DidHitMirror(other);
  }

  private bool DidHitTarget(Collider2D other) {
    Target target = other.transform.gameObject.GetComponent<Target>();
    if (target != null) {
      target.Explode();
      Poof();
      return true;
    }
    return false;
  }

  private void Poof() {
    Destroy(this.transform.gameObject);
  }

  private bool DidHitMirror(Collider2D other) {
    Mirror mirror = other.transform.gameObject.GetComponent<Mirror>();
    if (mirror != null) {
      mirror.OnHit();

      Mirror.MirrorType mirrorType = mirror.GetMirrorType();
      Vector2 newDirection; 
      if (mirrorType == Mirror.MirrorType.HORIZONTAL) {
        newDirection = new Vector2(this.direction.x * -1, this.direction.y);
      } else {
        newDirection = new Vector2(this.direction.x, this.direction.y * -1);
      }

      DirectToward(newDirection);
      return true;
    }
    return false;
  }
}
