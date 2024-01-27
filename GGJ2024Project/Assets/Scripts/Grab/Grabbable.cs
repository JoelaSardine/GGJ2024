using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    [SerializeField] private float Speed, AngularSpeed;
    
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
}
