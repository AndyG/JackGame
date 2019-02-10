using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitboxManager))]
public class Fireball : MonoBehaviour
{

  private HitboxManager hitboxManager;

  [SerializeField]
  private float velocity;

  private static HitInfo hitInfo = new HitInfo();

  void Awake()
  {
    hitboxManager = GetComponent<HitboxManager>();
  }

  void Update()
  {
    transform.Translate(transform.right * -1 * velocity * Time.deltaTime);

    List<Hurtable> hurtables = hitboxManager.GetOverlappedHurtables();
    if (hurtables.Count > 0)
    {
      Hurtable hurtable = hurtables[0];
      HurtInfo hurtInfo = hurtable.OnHit(hitInfo);
      Destroy(gameObject);
    }
  }
}
