using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;

public class Grabber : MonoBehaviour {

    [SerializeField]
    GameObject controller;
    [SerializeField]
    Transform anchor;

    private bool headSetOn = true;
    public GameObject _object;
    public Rigidbody rb_object;
    private bool grabbing;

    OVRInput.Controller activeController = OVRInput.GetActiveController();

    public Text txt;
    public Vector3 vel;
    private Vector3[] positions = new Vector3[100];

    public SearchBall _searchball;

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerOn;
        OVRManager.HMDUnmounted += PlayerOff;
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerOn;
        OVRManager.HMDUnmounted -= PlayerOff;


    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = Vector3.zero;
        }
    }


	
	// Update is called once per frame
	void Update () {

        if (!headSetOn)
        {
          //  return;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            CheckGrab();
        }

        if (grabbing)
        {
            UpdateGrabbing();
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
           

            if (grabbing)
            {
                Drop();
            }
        }

        SavePosition();

        CalculateVel();

       // txt.text = vel.ToString() + "\n" + this.transform.position.ToString() + "\n" + positions[Mathf.RoundToInt(OVRPlugin.GetAppFramerate())];

    }

    private void Grab()
    {
        grabbing = true;
        rb_object = _object.GetComponent<Rigidbody>();
        rb_object.isKinematic = true;
        return;
    }

    private void UpdateGrabbing()
    {
        rb_object.position = anchor.transform.position;
        rb_object.rotation = anchor.transform.rotation;
        //rb_object.velocity = this.transform.GetComponent<Rigidbody>().velocity;
    }

    private void Drop()
    {
       
        
        rb_object.isKinematic = false;
        _object.transform.parent = null;
        rb_object.velocity = vel*50f;
        rb_object.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(activeController);
            //Vector3.Normalize(OVRInput.GetLocalControllerAcceleration(activeController) * 50f); 
        //rb_object.AddForce(this.transform.forward * 100f, ForceMode.Impulse);
        _object = null;
        if (rb_object.gameObject.tag == "ball")
        {
            _searchball._ballThrowed = true;
        }
       
        rb_object = null;

        grabbing = false;
    }

    private bool CheckGrab()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position,transform.forward, out hit))
        {
            if (_object != hit.collider.gameObject)
            {
                _object = hit.transform.gameObject;
                if (_object.tag == "ball")
                {
                    Grab();
                }
            
            }
        }
        return true;
    }

    private void PlayerOn()
    {
        headSetOn = true;
    }

    private void PlayerOff()
    {
        headSetOn = false;
    }

    private void SavePosition()
    {
        for (int i = positions.Length - 1; i > 0; i--)
        {
            positions[i] = positions[i - 1];
        }
        positions[0] = this.transform.position;

    }

    private void CalculateVel()
    {
        vel = (this.transform.position - positions[1]) / 1f;
    }
}
