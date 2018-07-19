using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XylophoneHero.SongSystem;

namespace XylophoneHero
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(KeyAudioFeedback))]
    [RequireComponent(typeof(KeyVisualFeedback))]
    [RequireComponent(typeof(MusicalNote))]
    public class KeyCollisionDetect : MonoBehaviour
    {

        #region PRIVATE_MEMBER_VARIABLES
        public float ForceThreshold = 10.0f;
        public SongKeyIndicate KeyIndicator;
        #endregion

        #region PRIVATE_MEMBER_VARIABLES

        private KeyAudioFeedback m_KeyAudioFeedback;
        private KeyVisualFeedback m_KeyVisualFeedback;
        private MusicalNote m_MusicalNote;

        private bool m_KeyTouched = false;
        private int m_KeyTouchCount = 0;
        
        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOR_METHODS

        void Start()
        {
            m_KeyAudioFeedback = GetComponent<KeyAudioFeedback>();
            m_KeyVisualFeedback = GetComponent<KeyVisualFeedback>();
            m_MusicalNote = GetComponent<MusicalNote>();
        }

        #endregion // MONOBEHAVIOR_METHODS

        #region PUBLIC_METHODS

        private void OnTriggerEnter(Collider other)
        {
            if (!m_KeyTouched)
            {
                m_KeyVisualFeedback.MoveAndHighlightOnHit();
                m_KeyAudioFeedback.PlayKey(500f);
                m_MusicalNote.PublishMusicalNoteViaROS();
                m_MusicalNote.SendNoteOnMessage();
                if (KeyIndicator != null)
                {
                    KeyIndicator.HandleStrike();
                }
                m_KeyTouched = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            m_KeyTouched = false;
            m_MusicalNote.SendNoteOffMessage();
        }

        #endregion // PUBLIC_METHODS
    }

}