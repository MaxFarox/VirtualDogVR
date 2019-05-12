    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {


    private GameObject[] _scenes = new GameObject[3];

    [SerializeField]
    private GameObject _shower;
    [SerializeField]
    private GameObject _kitchen;
    [SerializeField]
    private GameObject _playground;
    private int _nextScene;
    private int _oldScene;

    private Transform[] _positions = new Transform[3];


    public Transform _center;
    public Transform _right;
    public Transform _left;

    public GameObject _manguera;
    public GameObject _grabber;



    // Use this for initialization
    void Start () {
        _nextScene = 2;
        _oldScene = 1;
        
        _scenes[0] = _shower;
        _scenes[1] = _kitchen;
        _scenes[2] = _playground;

        _positions[0] = _left;
        _positions[1] = _center;
        _positions[2] = _right;

        _scenes[0].transform.position = _left.position;
        _scenes[1].transform.position = _center.position;
        _scenes[2].transform.position = _right.position;

        _scenes[_nextScene].gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow) || OVRInput.GetUp(OVRInput.RawButton.DpadRight))
        {
            _oldScene = _nextScene;
            if (_nextScene + 1 > _scenes.Length-1)
            {
                _nextScene = 0;
            }
            else { _nextScene += 1; }
            ChangeScenario(_nextScene, _oldScene,0);
            
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || OVRInput.GetUp(OVRInput.RawButton.DpadLeft))
        {
            _oldScene = _nextScene;
            if (_nextScene - 1 < 0)
            {
                _nextScene = _scenes.Length-1;
            }
            else { _nextScene -= 1; }
            ChangeScenario(_nextScene, _oldScene,1);
           
          
        }
        if (_nextScene == 0)
        {
            _manguera.SetActive(true);
            _grabber.SetActive(false);
        }
        else {
            _manguera.SetActive(false);
            _grabber.SetActive(true);
        }
    }
    void ChangeScenario(int current, int old, int dir)
    {
        if (dir == 0)
        {
            _scenes[current].transform.position = Vector3.MoveTowards(_left.position, _center.position, 100);
            _scenes[current].gameObject.SetActive(true);
            _scenes[old].transform.position = Vector3.MoveTowards(_center.position, _right.position, 100);
            _scenes[old].gameObject.SetActive(false);

        }
        else {
            _scenes[current].transform.position = Vector3.MoveTowards(_right.position, _center.position, 100);
            _scenes[current].gameObject.SetActive(true);
            _scenes[old].transform.position = Vector3.MoveTowards(_center.position, _left.position, 100);
            _scenes[old].gameObject.SetActive(false);
        }
        
    }
}
