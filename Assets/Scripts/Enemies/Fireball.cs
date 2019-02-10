using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitboxManager))]
public class Fireball : MonoBehaviour
{

  [SerializeField]
  private bool debug = false;

  private HitboxManager hitboxManager;

  [SerializeField]
  private float velocity;

  private static HitInfo hitInfo = new HitInfo();
  private static Vector3 invertedLocalScale = new Vector3(-1f, 1f, 1f);

  void Awake()
  {
    hitboxManager = GetComponent<HitboxManager>();
  }

  void Update()
  {
    transform.Translate(-transform.right * velocity * Time.deltaTime, Space.World);
    if (debug)
    {
      Debug.Log(transform.right);
    }

    List<Hurtable> hurtables = hitboxManager.GetOverlappedHurtables();
    if (hurtables.Count > 0)
    {
      Hurtable hurtable = hurtables[0];
      HurtInfo hurtInfo = hurtable.OnHit(hitInfo);
      if (hurtInfo.hitConnected)
      {
        Destroy(gameObject);
      }
    }

    transform.localScale = velocity > 0 ? Vector3.one : invertedLocalScale;
  }
}
