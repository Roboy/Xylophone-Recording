using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XylophoneHero.SongSystem.Utils;

namespace XylophoneHero
{
    public class SongListBoard : MonoBehaviour
    {

        #region PRIVATE_MEMBER_VARIABLES

        private TextMeshPro m_SongListDisplay = null;

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOUR_METHODS

        private void Start()
        {
            m_SongListDisplay = transform.Find("SongListContent").GetComponent<TextMeshPro>();
        }

        #endregion // MONOBEHAVIOUR_METHODS


        #region PUBLIC_METHODS

        public void DisplaySongList(List<Song> songs, int currentSongIndex)
        {
            if (m_SongListDisplay != null)
            {
                string display = "";
                for (int i = 0; i < songs.Count; i++)
                {
                    if (i + 1 == currentSongIndex)
                    {
                        display = display + "- " + songs[i].Name + " -\n";

                    }
                    else
                    {
                        display = display + songs[i].Name + "\n";
                    }
                }
                m_SongListDisplay.text = display;
            }
        }

        public void DisplaySongList(bool isLoadSongFinish)
        {
            if (isLoadSongFinish)
            {
                //  this situation would not appear currently
            }
            else
            {
                //  only when fail to load songs would this method be called
                if (m_SongListDisplay != null)
                {
                    this.m_SongListDisplay.text = "Oops! Looks like sth wrong happened.";
                }
            }
        }

        #endregion // PUBLIC_METHODS
    }

}
