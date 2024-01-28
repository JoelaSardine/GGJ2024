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
	    if (_currentGrabable)
	    {
		    _currentGrabable.StopGrab();
		    _currentGrabable = null;
	    }
	    else if (_hoveringGrabable)
	    {
		    if (_hoveringGrabable.CanGrab)
		    {
			    _currentGrabable = _hoveringGrabable;
			    _hoveringGrabable = null;
			    _currentGrabable.StartGrab();
		    }
		    else
		    {
			    _hoveringGrabable.Interact();
			    OnInteractWith.Invoke(_hoveringGrabable);
		    }
		    
	    }
    }

    public void OnInteract()
    {
	    if (_currentGrabable)
	    {
		    _currentGrabable.Interact();
		    OnInteractWith.Invoke(_currentGrabable);
	    }
	    else if (_hoveringGrabable)
	    {
		    _hoveringGrabable.Interact();
		    OnInteractWith.Invoke(_hoveringGrabable);
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
	    }
	    else
	    {
		    GrabPointer.transform.localEulerAngles = Vector3.zero;
            
            Ray ray = new Ray(Visor.position, Visor.forward);
		    if (Physics.SphereCast(ray, GrabRadius, out RaycastHit hit, GrabDistance, Mask))
		    {
			    _hoveringGrabable = hit.rigidbody?.GetComponent<Grabable>();
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
