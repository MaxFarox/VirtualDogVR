using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchBall : MonoBehaviour {

    [SerializeField]
    private Transform _ball;

    [SerializeField]
    private Transform _origin;
    [SerializeField]
    private Transform _mouth;
    [SerializeField]
    private Transform _originBall;
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private Pet _pet;
    [SerializeField]
    private GameObject _playground;

    public float _speed;
    public bool _hasBall = false;
    public bool _ballThrowed = false;

    [SerializeField]
    private NavMeshAgent _agent;

    private float _amount;

    public Animator _animator;

    private 
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {


        if (_ballThrowed)
        {
            if (_hasBall)
            {
                if (Vector3.Distance(this.transform.position, _origin.position) > 0.5f)
                {
                    _ball.position = _mouth.position;
                    _agent.destination = _origin.position;

                    /* float step = _speed * Time.deltaTime;
                     this.transform.position = Vector3.MoveTowards(this.transform.position, _origin.position, _speed);
                     Vector3 newDir = Vector3.RotateTowards(this.transform.forward, _origin.position, step, 0.0f);
                     this.transform.rotation = Quaternion.LookRotation(newDir);*/
                }
                else
                {

                    _hasBall = false;
                    _ballThrowed = false;
                    _animator.SetBool("Moving", false);

                    _ball.GetComponent<Rigidbody>().isKinematic = false;
                    _ball.GetComponent<Rigidbody>().AddForce(this.transform.position - _camera.position, ForceMode.Impulse);
                    _ball.parent = _playground.transform;
                }
            }
            else
            {
                if (Vector3.Distance(this.transform.position, _ball.position) < 1.5f)
                {
                    _ball.GetComponent<Rigidbody>().isKinematic = true;
                    _ball.position = _mouth.position;
                    _hasBall = true;

                }
                else
                {
                    _animator.SetBool("Moving", true);
                    _agent.destination = _ball.position;
                    /* float step = _speed * Time.deltaTime;
                     this.transform.position = Vector3.MoveTowards(this.transform.position, _ball.position, _speed);
                     Vector3 newDir = Vector3.RotateTowards(this.transform.forward, _ball.position, step, 0.0f);
                     this.transform.rotation = Quaternion.LookRotation(newDir);*/
                }

            }


            _amount += 0.05f;
            if (_amount > 1)
            {
                _pet.SetHappy(1);
                _amount = 0;
            }


        }
        else {

            FaceTarget(_camera.position);
        }
       
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);
    }
}
