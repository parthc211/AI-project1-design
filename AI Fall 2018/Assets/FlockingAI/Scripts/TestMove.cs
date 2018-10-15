using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {

    public Rigidbody rb;
    public float s = 10.0f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * s);
    }
}
