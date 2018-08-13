using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero.SongSystem
{
    public class ToggleSongSystem : MonoBehaviour
    {

        public KeyCode TestKeyCode;
        public GameObject SongSystem;


        private void Update()
        {
            if (Input.GetKeyDown(TestKeyCode))
            {
                if (SongSystem != null)
                {
                    SongSystem.SetActive(!SongSystem.activeSelf);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (SongSystem != null)
            {
                SongSystem.SetActive(!SongSystem.activeSelf);
            }
        }
    }
}

