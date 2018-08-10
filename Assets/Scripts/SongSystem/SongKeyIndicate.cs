using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XylophoneHero.SongSystem
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(AudioSource))]
    public class SongKeyIndicate : MonoBehaviour
    {

        #region PUBLIC_MEMBER_VARIABLES

        public KeyCode TestKeyCode;

        public float HighlightTime = 0.1f;

        #endregion // PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES

        private GameObject m_SongNoteObject = null;
        private Renderer m_Rend;
        private AudioSource m_KeySound;

        private Color m_OriginColor;
        private Color m_CorrectColor = new Color(1.0f, 1.0f, 1.0f);
        private Color m_ErrorColor = new Color(0.8f, 0.2f, 0.2f);
        private const float m_Lerp = 0.6f;

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOR_METHODS
        void Start()
        {
            m_Rend = GetComponent<Renderer>();
            m_KeySound = GetComponent<AudioSource>();
            m_OriginColor = m_Rend.material.GetColor("_Color");

        }

        void Update()
        {
            if (Input.GetKeyDown(TestKeyCode))
            {
                HandleStrike();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "SongNote")
            {
                m_SongNoteObject = other.gameObject;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == m_SongNoteObject)
            {
                m_SongNoteObject = null;
            }
        }
        #endregion // MONOBEHAVIOR_METHODS

        #region PUBLIC_METHODS
        public void HandleStrike()
        {
            bool strikeResult = DestroySongNote();
            Color newColor;
            if (strikeResult)
            {
                newColor = Color.Lerp(m_OriginColor, m_CorrectColor, m_Lerp);
                SongManager.Instance.GoodHit();
            }
            else
            {
                newColor = m_ErrorColor;
                SongManager.Instance.BadHit();
            }
            StartCoroutine(changeKeyColor(newColor));
        }

        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS

        private IEnumerator changeKeyColor(Color color)
        {
            m_Rend.material.color = color;

            yield return new WaitForSeconds(HighlightTime);

            m_Rend.material.color = m_OriginColor;
        }

        private void playSound()
        {
            m_KeySound.Play();
        }

        private bool DestroySongNote()
        {
            if (m_SongNoteObject == null)
            {
                return false;
            }
            Destroy(m_SongNoteObject);
            return true;
        }

        #endregion // PRIVATE_METHODS
    }
}

