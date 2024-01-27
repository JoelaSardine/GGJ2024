using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    [SerializeField] private float Speed, AngularSpeed;

    [System.NonSerialized]
    public bool Hoovered = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FollowTarget(Transform target)
    {
        //TODO Faire la rotation
        Vector3 direction = target.position - transform.position;
        rb.velocity = direction * Speed;
    }

    public void StartGrab()
    {
        rb.useGravity = false;
    }

    public void StopGrab()
    {
        rb.useGravity = true;
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
