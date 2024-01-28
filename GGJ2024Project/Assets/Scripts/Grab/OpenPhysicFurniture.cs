using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPhysicFurniture : MonoBehaviour
{
    [SerializeField] private float OpenThreshold;
    [SerializeField] private Vector3 ImpulseAxis;
    
    private Vector3 pos;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
    }

    public void Trigger()
    {
        bool open = Vector3.Distance(pos, transform.position) > OpenThreshold;
        StartCoroutine(Impulse(open));
    }

    IEnumerator Impulse(bool open)
    {
        float timer = 0;
        while (timer < 0.25f)
        {
            timer += Time.deltaTime;
            rb.AddForce(transform.InverseTransformDirection(ImpulseAxis) * Time.deltaTime * (open ? -1 : 1), ForceMode.Impulse);
            yield return null;
        }
    }
}
