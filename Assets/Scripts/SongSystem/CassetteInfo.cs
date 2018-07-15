using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XylophoneHero.SongSystem.Utils;

namespace XylophoneHero
{
    public class CassetteInfo : MonoBehaviour
    {

        public Song CassetteSong;

        public void SetSong(Song song)
        {
            CassetteSong = song;
        }

    }
}

