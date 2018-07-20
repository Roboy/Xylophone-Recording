using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using XylophoneHero.SongSystem.Utils;
using ROSBridge;

namespace XylophoneHero.SongSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class SongManager : Singleton<SongManager>
    {


        #region PUBLIC_MEMBER_VARIABLES
        public float PrompShowTime = 0.5f;
        #endregion // PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES

        private GameObject m_InfoPromps;
        private TextMeshPro m_InfoPrompsText;
        private GameObject m_ScoreDisplay;
        private TextMeshPro m_ScoreDisplayText;

        private SongListBoard m_SongListBoard;

        private SongGenerator m_SongGenerator;


        private List<Song> m_Songs = null;
        private int m_SongCount = -1;
        private bool m_IsLoadSongFinish = false;
        private bool m_IsPlaying = false;
        private int m_CurrentSongIndex = -1;

        private int m_ComboCounter = 0;
        private int m_Score = 0;
        private string m_Promps = "";

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOR_METHODS

        private void Start()
        {

            m_InfoPromps = transform.Find("InfoPromps").gameObject;
            m_ScoreDisplay = transform.Find("ScoreDisplay").gameObject;
            m_SongListBoard = transform.Find("SongList").GetComponent<SongListBoard>();
            if (m_InfoPromps != null && m_ScoreDisplay != null)
            {
                m_InfoPrompsText = m_InfoPromps.GetComponent<TextMeshPro>();
                m_ScoreDisplayText = m_ScoreDisplay.GetComponent<TextMeshPro>();
            }
            m_SongGenerator = transform.Find("NoteGenerator").gameObject.GetComponent<SongGenerator>();

            LoadSong();
        }

        #endregion // MONOBEHAVIOR_METHODS

        #region PUBLIC_METHODS

        public void LoadSong()
        {

            if (m_Songs == null)
            {
                m_Songs = new List<Song>();
            }

            TextAsset[] rawSongFiles = Resources.LoadAll("Songs", typeof(TextAsset)).Cast<TextAsset>().ToArray();
            foreach (TextAsset rawSongData in rawSongFiles)
            {
                m_Songs.Add(new Song(rawSongData.name, new List<string>(rawSongData.text.Split('\n'))));

            }

            if (m_Songs.Count > 0)
            {
                m_SongCount = m_Songs.Count;
                m_CurrentSongIndex = 1;
                m_IsLoadSongFinish = true;
                m_SongListBoard.DisplaySongList(m_Songs, m_CurrentSongIndex);
            }
            else
            {
                m_SongCount = -1;
                m_CurrentSongIndex = -1;
                m_IsLoadSongFinish = false;
                m_SongListBoard.DisplaySongList(false);
            }
        }

        public void StartSong()
        {
            if (m_IsLoadSongFinish)
            {
                m_ComboCounter = 0;
                m_Score = 0;
                m_Promps = "";
                StartCoroutine(displayScoreText());
                m_SongGenerator.StartGenerate(m_Songs[m_CurrentSongIndex - 1]);
            }
        }

        public void PauseSong()
        {

        }

        public void StopSong()
        {
            m_SongGenerator.StopGenerate();
        }

        public void PrevSong()
        {
            --m_CurrentSongIndex;
            if (m_CurrentSongIndex <= 0)
            {
                m_CurrentSongIndex = m_SongCount;
            }
            StopSong();
            StartSong();
            m_SongListBoard.DisplaySongList(m_Songs, m_CurrentSongIndex);
        }

        public void NextSong()
        {
            ++m_CurrentSongIndex;
            if (m_CurrentSongIndex > m_SongCount)
            {
                m_CurrentSongIndex = 1;
            }
            StopSong();
            StartSong();
            m_SongListBoard.DisplaySongList(m_Songs, m_CurrentSongIndex);
        }

        public void GoodHit()
        {
            ++m_ComboCounter;
            if (m_ComboCounter < 5)
            {
                m_Score += 10;
                m_Promps = "Good!";
            }
            else if (m_ComboCounter < 10)
            {
                m_Score += 20;
                m_Promps = "Perfect!";
            }
            else if (m_ComboCounter < 15)
            {
                m_Score += 40;
                m_Promps = "Unbelievable!";
            }
            else
            {
                m_Score += 80;
                m_Promps = "Godlike!";
            }
            StartCoroutine(displayScoreText());
        }

        public void BadHit()
        {
            m_ComboCounter = 0;
            m_InfoPromps.SetActive(false);
        }

        public void SongFinish()
        {
            m_IsPlaying = false;
        }

        #endregion // PUBLIC_METHODS

        #region PROTECTED_METHODS
        protected SongManager() { }
        #endregion

        #region PRIVATE_METHODS

        private IEnumerator displayScoreText()
        {
            m_InfoPromps.SetActive(false);

            showScore();
            m_InfoPrompsText.SetText(m_Promps);

            m_InfoPromps.SetActive(true);

            yield return new WaitForSeconds(PrompShowTime);

            if (m_InfoPromps.activeSelf)
            {
                m_InfoPromps.SetActive(false);
            }
        }

        private void showScore()
        {
            m_ScoreDisplayText.SetText("Score: " + m_Score);
        }

        #endregion // PRIVATE_METHODS

    }
}