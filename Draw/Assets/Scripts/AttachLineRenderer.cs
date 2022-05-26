using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "attaches" a line renderer between two other game objects and also create
// a collider so you can cast raycasts on the line (or listen for collisions)
// only works for single-segment line renderers.
public class AttachLineRenderer : MonoBehaviour
{
    Vector3 start;
    Vector3 target;

    LineRenderer line;
    CapsuleCollider capsule;
    
    public float LineWidth; // use the same as you set in the line renderer.

    void Start()
    {
        line = GetComponent<LineRenderer>();
        capsule = gameObject.AddComponent<CapsuleCollider>();
        capsule.radius = LineWidth / 2;
        capsule.center = Vector3.zero;
        capsule.direction = 2; // Z-axis for easier "LookAt" orientation
    }

    void Update()
    {
        //line.SetPosition(0, start.position);
        //line.SetPosition(1, target.position);

        start = line.GetPosition(0);
        target = line.GetPosition(line.positionCount-1);
        
        capsule.transform.position = start + (target - start) / 2;
        capsule.transform.LookAt(start);
        capsule.height = (target - start).magnitude;
    }
}