using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OutgameBackground : MonoBehaviour
{
	[Range(0f, 1f)]
	public float hueSpeed = 0.1f;

	public int pow = 2;
	
	private Camera cam;
	private float h, s, v;

	// Start is called before the first frame update
	void Start()
    {
	    cam = GetComponent<Camera>();

	    Color.RGBToHSV(cam.backgroundColor, out h, out s, out v);
	    h = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
	    float hueDelta = Time.deltaTime * Mathf.Pow(hueSpeed, pow);
	    h = Mathf.Repeat(h + hueDelta, 1f);
	    cam.backgroundColor = Color.HSVToRGB(h, s, v);
    }
}
