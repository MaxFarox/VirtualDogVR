using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {

    [SerializeField]
    private Transform _ball;

    [SerializeField]
    private Transform _originPosition;

    [SerializeField]
    private Transform _controller;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (CheckButton())
            {
                _ball.position = _originPosition.position;
                _ball.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

    }

    private bool CheckButton()
    {
        RaycastHit hit;

        if (Physics.Raycast(_controller.position, _controller.forward, out hit))
        {
            if (this.gameObject == hit.collider.gameObject)
            {
                return true;

            }
        }
        return false;
        
    }
}
