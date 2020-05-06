using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XylophoneHero.SongSystem.Utils;


namespace XylophoneHero.SongSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class CassetteInfo : MonoBehaviour
    {

        private Song m_CassetteSong;
        private TextMeshPro m_SongName;

        private void Awake()
        {
            m_SongName = transform.Find("SongName").gameObject.GetComponent<TextMeshPro>();
        }


        public void SetSong(Song song)
        {
            m_CassetteSong = song;
            m_SongName.text = song.Name;
        }

        public Song GetSong()
        {
            return m_CassetteSong;
        }

    }
}

