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

        private AudioSource[] keySound;

        private float maxForce = 1000;

        void Start()
        {
            keySound = GetComponents<AudioSource>();
        }

        public void PlayKey(float force)
        {
            if (GameObject.FindGameObjectWithTag("cubeStick") != null)
            {
                playKey(keySound[0], force);
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("roboyStick") != null)
                {
                    playKey(keySound[1], force);
                }
                else
                {
                    if (GameObject.FindGameObjectWithTag("lightsaberStick") != null)
                    {
                        playKey(keySound[2], force);
                    }
                }
            }
        }

        private void playKey(AudioSource keySound, float force)
        {
            float vol = Mathf.Clamp(force / maxForce, 0.0f, 1.0f);
            keySound.volume = vol;
            keySound.Play();
        }
    }
}
