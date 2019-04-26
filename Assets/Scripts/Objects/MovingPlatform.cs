using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{

  [SerializeField]
  private float speed = 1f;
  [SerializeField]
  private float amplitude = 1f;

  [SerializeField]
  private float skinWidth = 0.015f;

  [Header("Ray Counts")]
  [SerializeField]
  protected int horizontalRayCount = 4;
  [SerializeField]
  protected int verticalRayCount = 4;

  [Header("LayerMask")]
  [SerializeField]
  protected LayerMask layerMask;

  private BoxCollider2D boxCollider;

  protected RaycastOrigins raycastOrigins;
  protected float horizontalRaySpacing = 0f;
  protected float verticalRaySpacing = 0f;

  // Start is called before the first frame update
  public virtual void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
    CalculateRaySpacing();
  }

  protected void UpdateRaycastOrigins()
  {
    Bounds bounds = boxCollider.bounds;
    // Inset the bounds by skin width.
    bounds.Expand(skinWidth * -2);

    raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
    raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
    raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
  }

  protected void CalculateRaySpacing()
  {
    Bounds bounds = boxCollider.bounds;
    // Inset the bounds by skin width.
    bounds.Expand(skinWidth * -2);

    horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
    verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

    horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
    verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
  }

  protected struct RaycastOrigins
  {
    public Vector2 topLeft, topRight, bottomLeft, bottomRight;
  }

  // Update is called once per frame
  void Update()
  {
    float newPosX = Mathf.Sin(Time.time * speed) * amplitude;
    Vector3 newPosition = new Vector3(newPosX, this.transform.position.y, this.transform.position.z);
    Vector3 velocity = newPosition - this.transform.position;
    this.transform.position = newPosition;
  }

  private HashSet<Transform> GetPassengers(Vector3 velocity)
  {
    HashSet<Transform> passengers = new HashSet<Transform>();
    // moving horizontally
    if (velocity.x != 0)
    {
      for ()
    }
    return passengers;
  }
}
