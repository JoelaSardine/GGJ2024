using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    [SerializeField] private Transform Visor, Target;
    [SerializeField] private float GrabDistance, GrabRadius;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private TMP_Text GrabText, GrabPointer;

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
	    this.hoveringGrabbable = null;

	    if (currentGrabbable)
	    {
		    GrabPointer.transform.localEulerAngles = Vector3.forward * 45f;

		    currentGrabbable.FollowTarget(Target);
		    if (!grab)
		    {
			    currentGrabbable.StopGrab();
			    currentGrabbable = null;
		    }
	    }
	    else
	    {
		    GrabPointer.transform.localEulerAngles = Vector3.zero;
            
            Ray ray = new Ray(Visor.position, Visor.forward);
		    if (Physics.SphereCast(ray, GrabRadius, out RaycastHit hit, GrabDistance, Mask))
		    {
			    hoveringGrabbable = hit.rigidbody?.GetComponent<Grabbable>();

			    if (grab && hoveringGrabbable)
			    {
				    currentGrabbable = hoveringGrabbable;
				    hoveringGrabbable = null;

				    currentGrabbable.StartGrab();
			    }
            }
	    }
        
	    // Refresh hoovering
        if (previousHovering && previousHovering != hoveringGrabbable)
        {
	        previousHovering.Hoovered = false;
        }

        if (hoveringGrabbable)
        {
	        hoveringGrabbable.Hoovered = true;
	        this.GrabText.text = $"Grab {hoveringGrabbable.name}";
        }
        else
        {
	        this.GrabText.text = string.Empty;
        }
    }

    void OnDrawGizmosSelected()
    {
	    Color transparent = new Color(1f, 1f, 1f, 0.5f);
	    
        Gizmos.color = Color.white * transparent;
        Gizmos.DrawSphere(Target.position, GrabRadius);
        
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
