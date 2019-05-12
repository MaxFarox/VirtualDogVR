using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStats : MonoBehaviour {

    [SerializeField]
    private Animator _animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad) || Input.GetKeyDown(KeyCode.O))
        {
            _animator.SetBool("openStats", true);
        }
        else {
            _animator.SetBool("openStats", true);
        }
	}
}
