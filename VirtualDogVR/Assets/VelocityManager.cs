using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityManager : MonoBehaviour {

    public Rigidbody rb;
    public GameObject controller;
    public Text txt;
    public Vector3 vel;
    private Vector3[] positions = new Vector3[100];


	// Use this for initialization
	void Start () {
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = Vector3.zero;
        }
	}
	
	// Update is called once per frame
	void Update () {

        rb.position = controller.transform.position;

        SavePosition();

        CalculateVel();
        
        txt.text = vel.ToString() + "\n" +  rb.position.ToString() + "\n" + positions[Mathf.RoundToInt(OVRPlugin.GetAppFramerate())];

	}

    private void SavePosition()
    {
        for (int i = positions.Length-1 ; i > 0; i--)
        {
            positions[i] = positions[i - 1];
        }
        positions[0] = rb.position;

    }

    private void CalculateVel()
    {
        vel = (rb.position - positions[Mathf.RoundToInt(OVRPlugin.GetAppFramerate())]) / 1f;
    }
}
