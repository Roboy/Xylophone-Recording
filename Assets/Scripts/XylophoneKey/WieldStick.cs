using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WieldStick : MonoBehaviour {

    public GameObject stickPrefab;
    public Rigidbody attachPoint;

    private GameObject stick;
    private FixedJoint joint;


	// Use this for initialization
    // generate the xylophone stick in the hand (controller)
	void Start () {

        Debug.Log("wield stick!!!!");

        stick = GameObject.Instantiate(stickPrefab);
        stick.transform.position = attachPoint.transform.position;
        stick.transform.Rotate(new Vector3(0.0f, 0.0f, 80.0f));
        stick.transform.Translate(new Vector3(0.0f, 0.2f, 0.0f));
        joint = stick.AddComponent<FixedJoint>();
        joint.connectedBody = attachPoint;
	}
	

}
