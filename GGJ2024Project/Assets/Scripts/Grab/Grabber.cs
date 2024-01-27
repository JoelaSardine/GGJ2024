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
	    Grabbable previousHovering = this.hoveringGrabbable;

        if (!currentGrabbable)
        {
            RaycastHit hit;
            Ray ray = new Ray(Visor.position, Visor.forward);
            if (Physics.SphereCast(ray, GrabRadius, out hit, GrabDistance, Mask))
            {
                hoveringGrabbable = hit.rigidbody?.GetComponent<Grabbable>();

                if (hoveringGrabbable)
                {
	                hoveringGrabbable.Hoovered = true;
                }
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

        if (previousHovering && previousHovering != hoveringGrabbable)
        {
	        previousHovering.Hoovered = false;
        }
    }

    void OnDrawGizmosSelected()
    {
	    Color transparent = new Color(1f, 1f, 1f, 0.5f);
	    
        Gizmos.color = Color.white * transparent;
        Gizmos.DrawSphere(Target.position, GrabRadius / 2f);
        
        if (currentGrabbable)
        {
	        Gizmos.color = Color.green;
        }
        else if (hoveringGrabbable)
        {
	        Gizmos.color = Color.yellow;
        }
        else
        {
	        Gizmos.color = Color.blue;
        }
        
        Gizmos.color *= transparent;
        Gizmos.DrawSphere(Visor.position + Visor.forward * GrabDistance, GrabRadius);
    }
}
