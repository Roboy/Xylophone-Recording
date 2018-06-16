using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero
{
    [RequireComponent(typeof(KeyAudioFeedback))]
    [RequireComponent(typeof(KeyVisualFeedback))]
    [RequireComponent(typeof(MusicalNote))]
    public class KeyCollisionDetect : MonoBehaviour
    {

        #region PRIVATE_MEMBER_VARIABLES
        public float ForceThreshold = 10.0f;
        public GameObject KeyIndicatorObject;
        #endregion

        #region PRIVATE_MEMBER_VARIABLES

        private KeyAudioFeedback m_KeyAudioFeedback;
        private KeyVisualFeedback m_KeyVisualFeedback;
        private MusicalNote m_MusicalNote;

        private bool m_KeyTouched = false;
        private int m_KeyTouchCount = 0;

        private SongKeyIndicate m_KeyIndicator;

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOR_METHODS

        void Start()
        {
            m_KeyAudioFeedback = GetComponent<KeyAudioFeedback>();
            m_KeyVisualFeedback = GetComponent<KeyVisualFeedback>();
            m_MusicalNote = GetComponent<MusicalNote>();
            if (KeyIndicatorObject != null)
            {
                m_KeyIndicator = KeyIndicatorObject.GetComponent<SongKeyIndicate>();
            }
        }

        #endregion // MONOBEHAVIOR_METHODS

        #region PUBLIC_METHODS

        /*
        void OnCollisionEnter(Collision collision)
        {
            if(!m_KeyTouched)
            {
                Vector3 impulseSum = collision.impulse;
                Vector3 forceSum = impulseSum / Time.fixedDeltaTime;
                float impactForce = Mathf.Max(forceSum.y, ForceThreshold);

                float relativeVelocity = collision.relativeVelocity.magnitude;
                Debug.Log("relativeVelocity " + relativeVelocity);

                m_KeyVisualFeedback.MoveAndHighlightOnHit();
                m_KeyAudioFeedback.PlayKey(impactForce);
                m_MusicalNote.publishMusicalNote();
                m_KeyTouched = true;
            }
        }

        void OnCollisionExit(Collision collision)
        {
            m_KeyTouched = false;
        }
        */

        private void OnTriggerEnter(Collider other)
        {
            if (!m_KeyTouched)
            {
                m_KeyVisualFeedback.MoveAndHighlightOnHit();
                m_KeyAudioFeedback.PlayKey(500f);
                m_MusicalNote.publishMusicalNoteViaROS();
                m_MusicalNote.sendNoteOnMessage();
                if (m_KeyIndicator != null)
                {
                    m_KeyIndicator.HandleStrike();
                }
                m_KeyTouched = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            m_KeyTouched = false;
            m_MusicalNote.sendNoteOffMessage();
        }

        #endregion // PUBLIC_METHODS
    }

}