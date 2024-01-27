using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    [SerializeField] private Transform Visor, Target;
    [SerializeField] private float GrabDistance, GrabRadius;
    [SerializeField] private LayerMask Mask;
    
    private Grabbable currentGrabbable, hoveringGrabbable;
    private bool grab;
    private PlayerInput input;
    
    void Start()
    {
    }

    public void OnGrab()
    {
        grab = !grab;
    }
    
    void Update()
    {
        if (!currentGrabbable)
        {
            RaycastHit hit;
            Ray ray = new Ray(Visor.position, Visor.forward);
            if (Physics.SphereCast(ray, GrabRadius, out hit, GrabDistance, Mask))
            {
                hoveringGrabbable = hit.rigidbody?.GetComponent<Grabbable>();
            }
            else
            {
                hoveringGrabbable = null;
            }

            if (grab && hoveringGrabbable)
            {
                currentGrabbable = hoveringGrabbable;
                currentGrabbable.StartGrab();
            }
        }
        else
        {
            currentGrabbable.FollowTarget(Target);
            if (!grab)
            {
                currentGrabbable.StopGrab();
                currentGrabbable = null;
            }
        }
    }
}
