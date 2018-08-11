using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XylophoneHero.SongSystem;

namespace XylophoneHero
{
    /// <summary>
    /// Looks for collisions on the notes and triggers the corresponding functions.
    /// If there is no VR Setup you can also debug via keyboard.
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
        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOR_METHODS

        void Start()
        {
            m_KeyAudioFeedback = GetComponent<KeyAudioFeedback>();
            m_KeyVisualFeedback = GetComponent<KeyVisualFeedback>();
            m_MusicalNote = GetComponent<MusicalNote>();
        }

        void Update()
        {
            if (Input.GetKeyDown(TestKeyCode))
            {
                OnTriggerEnter(new Collider());
            }
            if (Input.GetKeyUp(TestKeyCode))
            {
                OnTriggerExit(new Collider());
            }
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
            m_MusicalNote.SendNoteOffMessage();
            m_KeyTouched = false;
        }

        #endregion // PUBLIC_METHODS
    }

}