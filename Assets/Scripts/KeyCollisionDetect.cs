using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyAudioFeedback))]
[RequireComponent(typeof(KeyVisualFeedback))]
public class KeyCollisionDetect : MonoBehaviour {

    private KeyAudioFeedback keyAudioFeedback;
    private KeyVisualFeedback keyVisualFeedback;

    private bool keyTouched = false;

    private float forceThreshold = 2.0f;

	// Use this for initialization
	void Start () {
        keyAudioFeedback = GetComponent<KeyAudioFeedback>();
        keyVisualFeedback = GetComponent<KeyVisualFeedback>();

	}
	
    void OnCollisionEnter(Collision collision)
    {
        if(!keyTouched)
        {
            var impulseSum = collision.impulse;
            var forceSum = impulseSum / Time.fixedDeltaTime;
            if(forceSum.y > forceThreshold)
            {
                keyVisualFeedback.MoveAndHighlightOnHit();
                keyAudioFeedback.PlayKey(forceSum.y);
                keyTouched = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        keyTouched = false;
    }
}
