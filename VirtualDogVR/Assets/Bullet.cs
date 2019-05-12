using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        Invoke("DestroyMe", 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

   
    private void DestroyMe()
    {
        Destroy(this);
    }
}
