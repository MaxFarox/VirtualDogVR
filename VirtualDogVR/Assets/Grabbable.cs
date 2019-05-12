using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    private Rigidbody _rb;
	// Use this for initialization
	void Start () {
        _rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Vector3 GetVelocity()
    {
        return _rb.velocity;
    }
}
