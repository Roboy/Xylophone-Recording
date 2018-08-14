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

        private AudioClip m_ElectricGuitarJazz;
        private AudioClip m_Piano;
        private AudioClip m_Marimba;

        private AudioSource m_KeySound;
        private float m_MaxForce = 1000;

        void Start()
        {
            m_KeySound = GetComponent<AudioSource>();
            loadSounds();
        }

        public void PlayKey(float force, String stickTag)
        {
            Debug.Log(stickTag);
            if (stickTag == TagsConstants.CUBE_STICK)
            {
                playKey(m_Marimba, force);
                Debug.Log("Marimba");
            }
            else if (stickTag == TagsConstants.ROBOY_STICK)
            {
                playKey(m_Piano, force);
                Debug.Log("Piano");

            }
            else if (stickTag == TagsConstants.LIGHT_SABER_STICK)
            {
                playKey(m_ElectricGuitarJazz, force);
                Debug.Log("Electric");

            }
        }

        private void playKey(AudioClip sound, float force)
        {
            float vol = Mathf.Clamp(force / m_MaxForce, 0.0f, 1.0f);
            m_KeySound.volume = vol;
            m_KeySound.PlayOneShot(sound) ;
        }

        private void loadSounds()
        {
            string tone = Enum.GetName(typeof(KeyTones), Key);

            m_ElectricGuitarJazz = Resources.Load<AudioClip>("Notes/ElectricGuitarJazzMP3/ElectricGuitarJazz" + tone);
            m_Piano = Resources.Load<AudioClip>("Notes/PianoMP3/Piano" + tone);
            m_Marimba = Resources.Load<AudioClip>("Notes/MarimbaMP3/Marimba" + tone);

            Debug.Log(m_ElectricGuitarJazz);
            Debug.Log(m_Piano);
            Debug.Log(m_Marimba);
        }
    }
}
