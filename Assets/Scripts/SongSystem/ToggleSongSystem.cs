﻿using UnityEngine;
using XylophoneHero.SongSystem.Utils;

namespace XylophoneHero.SongSystem
{
    public class ToggleSongSystem : MonoBehaviour
    {

        public KeyCode TestKeyCode;
        public GameObject SongSystem;

        private ButtonAppearance m_Appearance;

        private void Start()
        {
            m_Appearance = GetComponent<ButtonAppearance>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(TestKeyCode))
            {
                if (SongSystem != null)
                {
                    SongSystem.SetActive(!SongSystem.activeSelf);
                    m_Appearance.SwitchColor(SongSystem.activeSelf);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (SongSystem != null)
            {
                SongSystem.SetActive(!SongSystem.activeSelf);
                m_Appearance.SwitchColor(SongSystem.activeSelf);
            }
        }
    }
}
