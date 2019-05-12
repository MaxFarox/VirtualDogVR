using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eat : MonoBehaviour {

    private GameObject[] _food;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Transform _foodSpawner;

    private int _hungryPoints;

    [SerializeField]
    private GameObject _apple;

    [SerializeField]
    private GameObject _appleButton;


    [SerializeField]
    private GameObject _chicken;

    [SerializeField]
    private GameObject _chickenButton;

    [SerializeField]
    private Transform _controller;

    [SerializeField]
    private Pet _pet;

    // Use this for initialization
    void Start () {
        _food = GameObject.FindGameObjectsWithTag("food");
	}
	
	// Update is called once per frame
	void Update () {

        _food = GameObject.FindGameObjectsWithTag("food");

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && _foodSpawner.gameObject.activeSelf)
        {
            RaycastHit hit;

            if (Physics.Raycast(_controller.position, _controller.forward, out hit))
            {
                if (_appleButton == hit.collider.gameObject)
                {
                    InstantiateFood(_apple);
                    _hungryPoints = 10;

                }
                else if (_chickenButton == hit.collider.gameObject)
                {
                    InstantiateFood(_chicken);
                    _hungryPoints = 20;
                }
            }
           
           
        }

        if (_food.Length > 0 && _foodSpawner.gameObject.activeSelf)
        {
            _animator.SetBool("eat", true);
            foreach (var food in _food)
            {
               
                Destroy(food, 5f);
            }
        }
        else {
            _animator.SetBool("eat", false);
        }
    }

    private void Feed()
    {
        _pet.SetHunger(_hungryPoints);
    }

    private void InstantiateFood(GameObject food)
    {
        if (_food.Length < 1)
        {
            Instantiate(food, _foodSpawner.position, Quaternion.identity);
            Invoke("Feed", 5f);
        }
       
    }
}
