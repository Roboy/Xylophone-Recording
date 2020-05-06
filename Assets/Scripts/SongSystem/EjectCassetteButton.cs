using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero.SongSystem
{
    public class EjectCassetteButton : DebounceButton
    {
        public CassettePlayerManeger Manager;


        private void OnTriggerEnter(Collider other)
        {
            if (m_IsButtonActivated)
            {
                if (Manager != null)
                {
                    Manager.EjectCasstte();
                }

                debounce();
            }
        }

    }
}
