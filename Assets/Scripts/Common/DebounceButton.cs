using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero
{
    public class DebounceButton : MonoBehaviour
    {
        protected bool m_IsButtonActivated = true;

        private float m_DebounceWaitTime = 1.5f;

        private IEnumerator debounceDelay()
        {
            m_IsButtonActivated = false;
            yield return new WaitForSeconds(m_DebounceWaitTime);
            m_IsButtonActivated = true;
        }

        protected void debounce()
        {
            StartCoroutine(debounceDelay());
        }
    }

}
