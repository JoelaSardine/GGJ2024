using System;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Path = DG.Tweening.Plugins.Core.PathCore.Path;

public class Grabber : MonoBehaviour
{
    [SerializeField] private Transform Visor, Target, baseTarget;
    [SerializeField] private float GrabDistance, GrabRadius;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private TMP_Text GrabText, GrabPointer;
    [SerializeField] private Transform[] AnimTarget;

    private Grabable _currentGrabable, _hoveringGrabable;
    private bool grab, interact;
    private PlayerInput input;

    public UnityEvent<Grabable> OnInteractWith;

    private DG.Tweening.Sequence tweenSequence;

    public bool ForceGrab(Grabable grabable)
    {
	    if (grabable.CanGrab && !_currentGrabable)
	    {
		    _currentGrabable = grabable;
		    _hoveringGrabable = null;
		    _currentGrabable.StartGrab();
		    return true;
	    }

	    return false;
    }
    
    public void AnimationInteract(int i, bool loop, bool destroy, bool delay)
    {
	    tweenSequence.Kill();
	    tweenSequence = DOTween.Sequence();
	    if (delay) tweenSequence.SetDelay(0.5f);
	    tweenSequence.Append(Target.DOLocalMove(AnimTarget[i].localPosition, 0.5f));
	    tweenSequence.Insert(0.0f, Target.DOLocalRotate(AnimTarget[i].localRotation.eulerAngles, 0.5f, RotateMode.Fast));
	    tweenSequence.AppendInterval(0.5f);
	    if (loop)
	    {
		    tweenSequence.Append(Target.DOLocalMove(baseTarget.localPosition, 0.25f));
		    tweenSequence.Insert(1.0f, Target.DOLocalRotate(baseTarget.localRotation.eulerAngles, 0.25f));
	    }
	    else
	    {
		    tweenSequence.onComplete = () =>
		    {
			    Target.position = baseTarget.position;
			    Target.rotation = baseTarget.rotation;
		    };
	    }

	    if (destroy)
	    {
		    tweenSequence.onComplete += () =>
		    {
			    if (_currentGrabable)
			    {
				    Destroy(_currentGrabable.gameObject);
			    }
		    };
	    }

	    tweenSequence.Play();
    }

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
			    _hoveringGrabable.Interact(this);
			    OnInteractWith.Invoke(_hoveringGrabable);
		    }
	    }
    }

    public void OnInteract()
    {
	    if (_currentGrabable)
	    {		    
		    OnInteractWith.Invoke(_currentGrabable);
		    _currentGrabable.Interact(this);

	    }
	    else if (_hoveringGrabable)
	    {
		    OnInteractWith.Invoke(_hoveringGrabable);
		    _hoveringGrabable.Interact(this);
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
	        this.GrabText.text = $"[E] : Grab {_hoveringGrabable.name}";
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
