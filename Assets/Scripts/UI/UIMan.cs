using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //The health bar will alwas face the user
       transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up); 
    }
}
