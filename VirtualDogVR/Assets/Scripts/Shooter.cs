using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {


    public GameObject bullet;
    public GameObject explosion;
    public float speed;
   

    private GameObject _bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _bullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
        _bullet.GetComponent<Rigidbody>().AddForce(this.transform.forward * speed, ForceMode.Force);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "target")
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddExplosionForce(750, hit.point, 250);
                Instantiate(explosion, hit.point, Quaternion.identity);
            }
        }

       
    }
}
