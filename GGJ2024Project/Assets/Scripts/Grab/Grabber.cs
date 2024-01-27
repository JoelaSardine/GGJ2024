using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    [SerializeField] private Transform Visor, Target;
    [SerializeField] private float GrabDistance, GrabRadius;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private TMP_Text GrabText, GrabPointer;

    private Grabable _currentGrabable, _hoveringGrabable;
    private bool grab, interact;
    private PlayerInput input;

    public UnityEvent<Grabable> OnInteractWith;
    
    public void OnGrab()
    {
        grab = !grab;
    }

    public void OnInteract()
    {
	    if (_currentGrabable)
	    {
		    _currentGrabable.Interact();
		    OnInteractWith.Invoke(_currentGrabable);
	    }
    }
    
    void Update()
    {
	    Grabable previousHovering = this._hoveringGrabable;
	    this._hoveringGrabable = null;

	    if (_currentGrabable)
	    {
		    GrabPointer.transform.localEulerAngles = Vector3.forward * 45f;

		    _currentGrabable.FollowTarget(Target);
		    if (!grab)
		    {
			    _currentGrabable.StopGrab();
			    _currentGrabable = null;
		    }
	    }
	    else
	    {
		    GrabPointer.transform.localEulerAngles = Vector3.zero;
            
            Ray ray = new Ray(Visor.position, Visor.forward);
		    if (Physics.SphereCast(ray, GrabRadius, out RaycastHit hit, GrabDistance, Mask))
		    {
			    _hoveringGrabable = hit.rigidbody?.GetComponent<Grabable>();

			    if (grab && _hoveringGrabable)
			    {
				    _currentGrabable = _hoveringGrabable;
				    _hoveringGrabable = null;

				    _currentGrabable.StartGrab();
			    }
            }
	    }
        
	    // Refresh hoovering
        if (previousHovering && previousHovering != _hoveringGrabable)
        {
	        previousHovering.Hoovered = false;
        }

        if (_hoveringGrabable)
        {
	        _hoveringGrabable.Hoovered = true;
	        this.GrabText.text = $"Grab {_hoveringGrabable.name}";
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
        
        if (_currentGrabable)
        {
	        Gizmos.color = Color.green;
        }
        else if (_hoveringGrabable)
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
