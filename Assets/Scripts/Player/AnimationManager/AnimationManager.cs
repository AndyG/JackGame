using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
  private Animator animator;
  private AnimationProvider animationProvider;

  void Start()
  {
    animator = GetComponent<Animator>();
    animationProvider = GetComponent<AnimationProvider>();
  }

  void Update()
  {
    string animation = animationProvider.GetAnimation();
    if (!animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals(animation))
    {
      Debug.Log("attempting to play animation: " + animation);
      animator.Play(animation);
    }
  }

  public interface AnimationProvider
  {
    string GetAnimation();
  }
}
