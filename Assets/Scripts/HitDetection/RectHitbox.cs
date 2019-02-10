using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectHitbox : MonoBehaviour
{
  [SerializeField]
  public float attackRangeX = 5;
  [SerializeField]
  public float attackRangeY = 3;

  public Collider2D[] GetColliders(LayerMask layerMask)
  {
    return Physics2D.OverlapBoxAll(transform.position, new Vector2(attackRangeX, attackRangeY), 0f, layerMask);
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(transform.position, new Vector3(attackRangeX, attackRangeY));
  }
}
