using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateGO : MonoBehaviour {

    //public GameObject mGO;
    // Use this for initialization
    public int rotationScale;

    public bool oscilacion = false;


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (oscilacion) {
            transform.Rotate(Vector3.up * (Mathf.Sin(Time.time - 180) * 0.1f ) , Space.World);
           // transform.Rotate(Vector3.down * (Mathf.Sin(Time.time - 180) * 0.1f), Space.World);
        }
        else {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationScale, Space.World);
        }
        

    }
}
