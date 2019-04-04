using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class SiphonDroplet : MonoBehaviour
{

  private Rigidbody2D rb;
  private Animator animator;

  public void Awake()
  {
    this.rb = GetComponent<Rigidbody2D>();
    this.animator = GetComponent<Animator>();
  }

  public void OnEnable()
  {
    // set animator to show normal droplet
  }

  public void AttractToward(Vector2 targetPosition)
  {
    Vector3 direction = ((Vector3)targetPosition - transform.position).normalized;
    rb.MovePosition(transform.position + direction * Time.deltaTime);
  }
}
