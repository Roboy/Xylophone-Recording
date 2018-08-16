using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XylophoneHero.SongSystem
{
    public class ReloadSongButton : DebounceButton
    {
        private void OnTriggerEnter(Collider other)
        {
            if (m_IsButtonActivated)
            {
                SongManager.Instance.ReloadSong();
            }

            debounce();
        }

    }
}
