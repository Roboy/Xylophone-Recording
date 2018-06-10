using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyAudioFeedback))]
[RequireComponent(typeof(KeyVisualFeedback))]
[RequireComponent(typeof(MusicalNote))]
public class KeyCollisionDetect : MonoBehaviour {

    #region PRIVATE_MEMBER_VARIABLES
    private KeyAudioFeedback keyAudioFeedback;
    private KeyVisualFeedback keyVisualFeedback;
    private MusicalNote m_MusicalNote;

    private bool keyTouched = false;
    private int keyTouchCount = 0;
    private float forceThreshold = 2.0f;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS
	void Start () {
        keyAudioFeedback = GetComponent<KeyAudioFeedback>();
        keyVisualFeedback = GetComponent<KeyVisualFeedback>();
        m_MusicalNote = GetComponent<MusicalNote>();
	}
    #endregion // MONOBEHAVIOR_METHODS
	
    #region PUBLIC_METHODS
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
                m_MusicalNote.publishMusicalNote();
                keyTouched = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (keyTouchCount <= 0)
        {
            keyVisualFeedback.MoveAndHighlightOnHit();
            keyAudioFeedback.PlayKey(1000f);
            m_MusicalNote.publishMusicalNote();
            keyTouchCount += 1;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        keyTouched = false;
    }

    void OnTriggerExit(Collider other)
    {
        if(keyTouchCount > 0)
        {
            keyTouchCount -= 1;
        }
        else
        {
            keyTouchCount = 0;
        }
    }
    #endregion // PUBLIC_METHODS
}
