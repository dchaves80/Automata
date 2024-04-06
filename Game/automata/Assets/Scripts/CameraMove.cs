using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Camera>().orthographicSize += Input.GetAxis("Mouse ScrollWheel")*-1f;
        
        if (GlobalVars.global_LockMapScroll == false) { 
        if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * 0.2f);
        if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * 0.2f);
        if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up * 0.2f);
        if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down * 0.2f);
        }

    }
}
