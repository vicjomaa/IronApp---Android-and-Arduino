using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVizCam : MonoBehaviour {

    WebCamTexture mWebCam;

	// Use this for initialization
	void Start () {
        mWebCam = new WebCamTexture();
        Renderer mRenderer = GetComponent<Renderer>();
        mRenderer.material.mainTexture = mWebCam;
        mWebCam.Play();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
