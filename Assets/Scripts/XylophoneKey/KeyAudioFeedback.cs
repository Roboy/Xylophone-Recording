using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class KeyAudioFeedback : MonoBehaviour
    {

        public string Key;

        private AudioSource m_KeySound;

        private float m_MaxForce = 1000;

        void Start()
        {
            m_KeySound = GetComponent<AudioSource>();
        }

        public void PlayKey(float force)
        {
            if (GameObject.FindGameObjectWithTag("cubeStick") != null)
            {
                playKey(m_KeySound, force);
            }
        }

        private void playKey(AudioSource keySound, float force)
        {
            float vol = Mathf.Clamp(force / m_MaxForce, 0.0f, 1.0f);
            keySound.volume = vol;
            keySound.Play();
        }
    }
}
