
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++) {
            Debug.Log(i + " "+ devices[i].name);
        }

        Renderer rend = this.GetComponentInChildren<Renderer>();

        WebCamTexture myCam = new WebCamTexture();
        string camName = devices[1].name;
        myCam.deviceName = camName;
        rend.material.mainTexture = myCam;

        myCam.Play();
	}
}
