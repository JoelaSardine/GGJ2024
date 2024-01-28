using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Grabable : MonoBehaviour
{
    [SerializeField] private float Speed, AngularSpeed;
    [SerializeField] private UnityEvent<Grabber> OnInteract;
    public bool CanGrab = true;

    [System.NonSerialized]
    public bool Hoovered = false;

    private Rigidbody rb;
    private int initialLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialLayer = gameObject.layer;
    }

    public void FollowTarget(Transform target)
    {
        float angle = 0;
        Vector3 axis = Vector3.zero;
        
        (target.rotation * Quaternion.Inverse(transform.rotation)).ToAngleAxis(out angle, out axis);

        Debug.Log(axis + " : " + angle);
        
        if (angle > 180)
        {
            axis *= -1;
            angle = 360 -angle;
        }

        rb.angularVelocity = axis * AngularSpeed * angle;
        
        Vector3 direction = target.position - transform.position;
        rb.velocity = direction * Speed;
    }

    public void StartGrab()
    {
	    gameObject.layer = LayerMask.NameToLayer("Grabbed");
        rb.useGravity = false;
    }

    public void StopGrab()
    {
        rb.useGravity = true;
        gameObject.layer = initialLayer;
    }

    public void Interact(Grabber grabber)
    {
        OnInteract.Invoke(grabber);
    }

    public void OnDrawGizmos()
    {
	    if (Hoovered)
	    {
		    float gizmoSize = 2f;
		    Gizmos.color = Color.yellow;
		    Gizmos.DrawLine(transform.position, transform.position + Vector3.up * gizmoSize);
		    Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * gizmoSize);
		    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * gizmoSize);
        }
    }
}
