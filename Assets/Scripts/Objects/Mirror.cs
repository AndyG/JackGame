using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Mirror : MonoBehaviour
{

    [SerializeField]
    private MirrorType mirrorType;

    private Animator animator;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void OnHit() {
        animator.SetBool("IsBouncing", true);
    }

    void OnBounceComplete() {
        animator.SetBool("IsBouncing", false);
    }

    public MirrorType GetMirrorType() {
        return mirrorType;
    }

    public enum MirrorType {
        HORIZONTAL,
        VERTICAL
    }
}
