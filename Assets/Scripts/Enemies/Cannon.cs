using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Cannon : MonoBehaviour
{

  private Spawner spawner;
  private Animator animator;

  [SerializeField]
  private float interval = 1;

  private float timeUntilShot;
  void Start()
  {
    spawner = GetComponentInChildren<Spawner>();
    animator = GetComponent<Animator>();

    timeUntilShot = interval;
  }

  void Update()
  {
    timeUntilShot -= Time.deltaTime;
    if (timeUntilShot <= 0)
    {
      DoShoot();
      timeUntilShot = interval;
    }
  }

  private void DoShoot()
  {
    animator.SetTrigger("Shoot");
    spawner.Spawn(transform.rotation);
  }
}
