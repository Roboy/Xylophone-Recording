using UnityEngine;

public class WebCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
        WebCamDevice[] cameraDevices = WebCamTexture.devices;

        Renderer rend = this.GetComponentInChildren<Renderer>();

        WebCamTexture myCamera = new WebCamTexture();
        string cameraName = cameraDevices[2].name;
        myCamera.deviceName = cameraName;
        rend.material.mainTexture = myCamera;
        myCamera.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
