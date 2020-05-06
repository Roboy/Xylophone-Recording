using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero.SongSystem.Utils
{
    public class ButtonAppearance : MonoBehaviour
    {

        public Color OverrideRingColor = Color.clear;

        public bool OffWhenStart = false;

        private Color m_OffRingColor = new Color(1f, 0.214f, 0.164f, 1f);
        private float m_Intensity = 2f;
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
                        m_OriginalEmissionColor = m.GetColor("_EmissionColor");
                        m_EmissiveMaterial.EnableKeyword("_EMISSION");
                        if (OffWhenStart)
                        {
                            m.SetColor("_EmissionColor", m_OffRingColor * m_Intensity);
                        }
                        else if (OverrideRingColor != Color.clear)
                        {
                            m.SetColor("_EmissionColor", OverrideRingColor * m_Intensity);
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
                    m_EmissiveMaterial.SetColor("_EmissionColor", OverrideRingColor * m_Intensity);
                }
                else
                {
                    m_EmissiveMaterial.SetColor("_EmissionColor", m_OriginalEmissionColor * m_Intensity);
                }
            }
            else
            {
                m_EmissiveMaterial.SetColor("_EmissionColor", m_OffRingColor * m_Intensity);
            }

        }
    }
}

