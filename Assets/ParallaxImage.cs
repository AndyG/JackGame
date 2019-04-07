using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxImage : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float parallaxScale = 1f; 

    private Vector3 lastTargetPosition;

    private bool initialized = false;

    void Start() {
        initialized = false;
        TrackTarget();
    }

    // Update is called once per frame
    void Update()
    {
        TrackTarget();
    }

    void OnValidate() {
        TrackTarget();
    }

    private void TrackTarget() {
        if (!initialized) {
            lastTargetPosition = target.position;
            initialized = true;
        } else {
            Vector3 delta = lastTargetPosition - target.position;
            Vector3 movement = delta * parallaxScale;
            this.transform.position = this.transform.position + (delta * parallaxScale);
            lastTargetPosition = target.position;
        }
    }
}
