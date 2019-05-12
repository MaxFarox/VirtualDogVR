using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manguera : MonoBehaviour {

    public ParticleSystem _water;
    private bool _waterOn = false;
    private bool _waterColl = false;
    public Pet _pet;
    private float _amount;
   

    List<ParticleCollisionEvent> collisionEvents;
    // Use this for initialization
    void Start () {
        collisionEvents = new List<ParticleCollisionEvent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButtonDown("Fire1"))
        {
            _waterOn = true;
            //_water.Emit(10);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButtonUp("Fire1"))
        {
            _waterOn = false;
            //_water.Emit(10);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            //_water.Emit(10);
           // _waterOn = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
           // _waterOn = false;
        }
        if (_waterOn)
        {
            Debug.Log("Emmiting water");
            _water.Emit(4);
        }

        if (_waterColl && _waterOn)
        {
            _amount += 0.1f;
            if (_amount > 1)
            {
                _pet.SetClean(1);
                _amount = 0;
            }

        }
        else { _amount = 0; }

        _waterColl = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(_water, other, collisionEvents);
        if (other.gameObject.tag == "pet")
        {
            Debug.Log("WATTTER");
            _waterColl = true;
        }
       

    }
}
