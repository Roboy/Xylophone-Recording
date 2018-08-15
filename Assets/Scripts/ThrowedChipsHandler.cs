using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowedChipsHandler : MonoBehaviour {

    private Vector3 firstPosition;
	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 chipsPos = this.transform.localPosition;

        chipsPos = new Vector3(chipsPos.x, chipsPos.y+1,
            chipsPos.z);
        GameObject controller = GameObject.FindGameObjectWithTag(TagsConstants.CONTROLLER_DEVICE);
        if (Mathf.Abs(controller.transform.position.y - chipsPos.y) > 10) {
            Destroy(this);


        }
    }
    

}
