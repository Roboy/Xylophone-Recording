using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero.SongSystem.Utils
{
    public class ButtonAppearance : MonoBehaviour
    {

        public Color OverrideRingColor = Color.clear;

        private Renderer m_Rend;

        void Start()
        {

            m_Rend = GetComponent<Renderer>();

            if (OverrideRingColor != Color.clear && m_Rend != null)
            {
                foreach (Material m in m_Rend.materials)
                {
                    if (m.name == "ControlButtonSecondaryMaterial (Instance)")
                    {
                        m.SetColor("_EmissionColor", OverrideRingColor);
                    }
                }
            }
        }
    }
}

