using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using XylophoneHero.SongSystem.Utils;
using ROSBridge;
using System;

namespace XylophoneHero.SongSystem
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SongManager : Singleton<SongManager>
    {
        
        #region PUBLIC_MEMBER_VARIABLES
        public float PrompShowTime = 0.5f;

        public GameObject Desk;
        public GameObject SongCassettePrefab;

        #endregion // PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES

        private GameObject m_InfoPromps;
        private TextMeshPro m_InfoPrompsText;
        private GameObject m_ScoreDisplay;
        private TextMeshPro m_ScoreDisplayText;

        private SongListBoard m_SongListBoard;

        private SongGenerator m_SongGenerator;

        private AudioSource m_AudioSource;
        private AudioClip m_CheeringSound;
        private AudioClip m_BooingSound;

        private List<Song> m_Songs = null;
        private int m_SongCount = -1;
        private bool m_IsLoadSongFinish = false;
        private bool m_IsPlaying = false;
        private int m_CurrentSongIndex = -1;

        private int m_ComboCounter = 0;
        private int m_MissCounter = 0;
        private int m_Score = 0;
        private string m_Promps = "";

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOR_METHODS

        private void Start()
        {

            Debug.Log("Song System Manager Start");

            m_InfoPromps = transform.Find("InfoPromps").gameObject;
            m_ScoreDisplay = transform.Find("ScoreDisplay").gameObject;
            m_SongListBoard = transform.Find("SongList").GetComponent<SongListBoard>();
            if (m_InfoPromps != null && m_ScoreDisplay != null)
            {
                m_InfoPrompsText = m_InfoPromps.GetComponent<TextMeshPro>();
                m_ScoreDisplayText = m_ScoreDisplay.GetComponent<TextMeshPro>();
            }
            m_SongGenerator = transform.Find("NoteGenerator").gameObject.GetComponent<SongGenerator>();

            m_AudioSource = GetComponent<AudioSource>();

            LoadSong();

            LoadSoundEffects();
        }

        private void OnEnable()
        {
            Debug.Log("Song System Manager Enable");

        }

        private void OnDisable()
        {
            Debug.Log("Song System Manager Disable");

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


            //  generate a bunch of cassettes, each corresponds to one song

            Vector3 deskPosition = Desk.transform.position;
            Vector3 cassetteOffset = new Vector3(0f, 0f, 0.2f);

            foreach (Song s in m_Songs)
            {
                GameObject cassette = GameObject.Instantiate<GameObject>(SongCassettePrefab, transform.parent.transform, false);
                CassetteInfo info = cassette.GetComponent<CassetteInfo>();
                info.SetSong(s);
                cassette.transform.SetPositionAndRotation(deskPosition + cassetteOffset, Quaternion.Euler(0f, -90f, 0f));
                cassetteOffset.x += 0.2f;
            }


        }

        //  start the song of m_CurrentSongIndex
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

        //  start the song passed to the function
        public void StartSong(Song song)
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
            m_MissCounter = 0;
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
                cheer();
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
            ++m_MissCounter;
            if(m_MissCounter >= 10)
            {
                boo();
            }
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

        private void cheer()
        {
            if(m_AudioSource != null)
            {
                m_AudioSource.PlayOneShot(m_CheeringSound);
            }
        }

        private void boo()
        {
            if(m_AudioSource != null)
            {
                m_AudioSource.PlayOneShot(m_BooingSound);
            }
        }

        private void LoadSoundEffects()
        {
            m_CheeringSound = Resources.Load<AudioClip>("SoundEffects/cheering");
            m_BooingSound = Resources.Load<AudioClip>("SoundEffects/booing");
        }
        #endregion // PRIVATE_METHODS

    }
}