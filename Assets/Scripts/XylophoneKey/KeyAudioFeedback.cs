using System;
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

        public enum KeyTones
        {
            C5, 
            CSharp5, 
            D5, 
            DSharp5, 
            E5,
            F5, 
            FSharp5, 
            G5, 
            GSharp5, 
            A5, 
            ASharp5, 
            B5, 
            C6, 
            CSharp6, 
            D6, 
            DSharp6, 
            E6, 
            F6, 
            FSharp6, 
            G6, 
            GSharp6, 
            A6, 
            ASharp6, 
            B6, 
            C7, 
            CSharp7, 
            D7, 
            DSharp7, 
            E7, 
            F7, 
            FSharp7, 
            G7, 
            GSharp7, 
            A7, 
            ASharp7, 
            B7, 
            C8
        }

        public KeyTones Key;

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
