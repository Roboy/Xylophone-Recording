using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyAudioFeedback))]
[RequireComponent(typeof(KeyVisualFeedback))]
public class KeyCollisionDetect : MonoBehaviour {

    private KeyAudioFeedback keyAudioFeedback;
    private KeyVisualFeedback keyVisualFeedback;


	// Use this for initialization
	void Start () {
        keyAudioFeedback = GetComponent<KeyAudioFeedback>();
        keyVisualFeedback = GetComponent<KeyVisualFeedback>();

	}
	
    void OnCollisionEnter(Collision collision)
    {
        keyVisualFeedback.MoveAndHighlightOnHit();
        keyAudioFeedback.PlayKey();
    }
}
