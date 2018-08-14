using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero.SongSystem.Utils
{
    public class ButtonAppearance : MonoBehaviour
    {

        public Color OverrideRingColor = Color.clear;
        public Color OffRingColor = new Color(245f, 94f, 94f);

        public bool OffWhenStart = false;

        private Renderer m_Rend;
        private Material m_EmissiveMaterial;
        private Color m_OriginalEmissionColor;

        private void Start()
        {
            m_Rend = GetComponent<Renderer>();
            if(m_Rend != null)
            {
                foreach (Material m in m_Rend.materials)
                {
                    if (m.name == "ControlButtonSecondaryMaterial (Instance)")
                    {
                        m_EmissiveMaterial = m;
                        m_OriginalEmissionColor = m.GetColor("_EmssionColor");

                        if (OffWhenStart)
                        {
                            m.SetColor("_EmissionColor", OffRingColor);
                        }
                        else if (OverrideRingColor != Color.clear)
                        {
                            m.SetColor("_EmissionColor", OverrideRingColor);
                        }

                    }
                }
            }
        }

        public void SwitchColor(bool active)
        {
            if (active)
            {
                if (OverrideRingColor != Color.clear)
                {
                    m_EmissiveMaterial.SetColor("_EmissionColor", OverrideRingColor);
                }
                else
                {
                    m_EmissiveMaterial.SetColor("_EmissionColor", m_OriginalEmissionColor);
                }
            }
            else
            {
                m_EmissiveMaterial.SetColor("_EmissionColor", OffRingColor);
            }

        }
    }
}

