using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManfredSiphonActive : ManfredStates.ManfredState0Param
{

  [SerializeField]
  private float siphonRadius = 3f;
  [SerializeField]
  private LayerMask siphonLayerMask; 
  [SerializeField]
  private float attractForce = 0.1f;
  [SerializeField]
  private float collectDistanceRadius = 0.1f;

  public override void Tick()
  {
    if (!manfred.playerInput.GetIsHoldingSiphon())
    {
      TransitionToSiphonRecovery();
      return;
    }

    Collider2D[] colliders = Physics2D.OverlapCircleAll(manfred.siphonSinkTransform.position, siphonRadius, siphonLayerMask);
    for (int i = 0; i < colliders.Length; i++) {
      Collider2D collider = colliders[i];
      SiphonSource source = collider.GetComponent<SiphonSource>();
      if (source != null) {
          source.OnSiphoned(manfred.siphonSinkTransform.position, attractForce);

          SiphonDroplet droplet = source as SiphonDroplet;
          if (droplet != null && ShouldCollectDroplet(droplet)) {
            CollectDroplet(droplet);
          }
        }
    }
  }

  public override string GetAnimation()
  {
    return "ManfredSiphonActive";
  }

  private bool ShouldCollectDroplet(SiphonDroplet droplet) {
    float distance = (droplet.transform.position - manfred.siphonSinkTransform.position).magnitude;
    return distance < collectDistanceRadius;
  }

  private void CollectDroplet(SiphonDroplet droplet) {
    droplet.OnCollected();
    manfred.cardManager.AddPercentToCard(droplet.GetPercentContained());
  }

  private void TransitionToSiphonRecovery()
  {
    // This is a hack and should be done smarter by storing the currently attracted droplets and notifying them without finding them again.
    Collider2D[] colliders = Physics2D.OverlapCircleAll(manfred.siphonSinkTransform.position, siphonRadius * 2, siphonLayerMask);
    for (int i = 0; i < colliders.Length; i++) {
      Collider2D collider = colliders[i];
      SiphonDroplet droplet = collider.GetComponent<SiphonDroplet>();
      if (droplet != null) {
        droplet.OnAttractionStopped();
      }
    }

    manfred.fsm.ChangeState(manfred.stateSiphonRecovery, manfred.stateSiphonRecovery);
  }
}
