using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitboxManager))]
public class Fireball : MonoBehaviour
{

  private HitboxManager hitboxManager;

  [SerializeField]
  private float velocity;

  void Awake()
  {
    hitboxManager = GetComponent<HitboxManager>();
  }

  void Update()
  {
    List<Hurtable> hurtables = hitboxManager.GetOverlappedHurtables();
    if (hurtables.Count > 0)
    {
      Debug.Log("fireball hit something!");
    }
    transform.Translate(transform.right * -1 * velocity * Time.deltaTime);
  }
}
