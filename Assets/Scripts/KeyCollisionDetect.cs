using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollisionDetect : MonoBehaviour {

    private KeyAudioFeedback keyAudioFeedback;
    private KeyVisualFeedback keyVisualFeedback;


	// Use this for initialization
	void Start () {
        keyAudioFeedback = GetComponent<KeyAudioFeedback>();
        keyVisualFeedback = GetComponent<KeyVisualFeedback>();

	}
	
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("aaah!");
        keyAudioFeedback.PlayKey();
        keyVisualFeedback.MoveAndHighlightOnHit();
    }
}
