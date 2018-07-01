using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class SongGenerator : MonoBehaviour {

    #region PUBLIC_MEMBER_VARIABLES

    public string SongFolderPath;

    //  the x coordinate of the corresponding note
    //  the program will generate the note at this position
    public float PositionC = 1.5f;
    public float PositionD = 1.0f;
    public float PositionE = 0.5f;
    public float PositionF = 0f;
    public float PositionG = -0.5f;
    public float PositionA = -1.0f;
    public float PositionB = -1.5f;

    public float Bpm = 120f;

    public TextMeshPro SongListDisplay;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES

    [SerializeField] private GameObject m_SongNotePrefab;

    private List<string> m_SongNameList = null;
    private List<List<string>> m_SongDataList = null;
    
    private bool m_LoadSongFinish = false;
    private bool m_Playing = false;
    private int m_CurrentSongIndex = -1;

    private Coroutine m_GenerationCoroutine = null;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS

    #endregion // MONOBEHAVIOR_METHODS

    #region PUBLIC_METHODS

    public void StartGenerate()
    {
        if (!m_LoadSongFinish)
        {
            LoadSong();
        }
        if (m_LoadSongFinish && !m_Playing)
        {
            m_Playing = true;
            m_GenerationCoroutine = StartCoroutine(generateNotes());
        }
    }

    public void PauseGenerate()
    {

    }

    public void StopGenerate()
    {
        if(m_GenerationCoroutine != null)
        {
            killAllNotes();
            StopCoroutine(m_GenerationCoroutine);
            m_Playing = false;
        }
    }

    public void GenerateNext()
    {

    }

    public void GeneratePrev()
    {

    }

    public void LoadSong()
    {
        if (m_SongDataList == null && m_SongNameList == null)
        {
            m_SongDataList = new List<List<string>>();
            m_SongNameList = new List<string>();
        }
        
        TextAsset[] rawSongFiles = Resources.LoadAll("Songs", typeof(TextAsset)).Cast<TextAsset>().ToArray();
        foreach (TextAsset rawSongData in rawSongFiles)
        {
            m_SongNameList.Add(rawSongData.name);
            List<string> songData = new List<string>(rawSongData.text.Split('\n'));
            m_SongDataList.Add(songData);
        }

        if (m_SongDataList.Count > 0)
        {
            m_CurrentSongIndex = 1;
            m_LoadSongFinish = true;
        }
        else
        {
            m_CurrentSongIndex = -1;
            m_LoadSongFinish = false;
        }


    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    private IEnumerator generateNotes()
    {
        float noteInterval = 60 / Bpm;
        foreach(string line in m_SongDataList[m_CurrentSongIndex-1])
        {
            if (line[0] == '1')
            {
                generateNote(PositionC);
            }
            if (line[1] == '1')
            {
                generateNote(PositionD);
            }
            if (line[2] == '1')
            {
                generateNote(PositionE);
            }
            if (line[3] == '1')
            {
                generateNote(PositionF);
            }
            if (line[4] == '1')
            {
                generateNote(PositionG);
            }
            if (line[5] == '1')
            {
                generateNote(PositionA);
            }
            if (line[6] == '1')
            {
                generateNote(PositionB);
            }
            yield return new WaitForSeconds(noteInterval);
        }
        m_Playing = false;
    }

    private void generateNote(float posx)
    {
        GameObject n = Instantiate(m_SongNotePrefab, transform.parent.transform, false);
        n.transform.localPosition = new Vector3(posx, 5.0f, 0.05f);
        n.transform.Rotate(new Vector3(90, 0, 0));
    }

    private void killAllNotes()
    {
        GameObject[] songNotes = GameObject.FindGameObjectsWithTag("SongNote");
        foreach (GameObject sn in songNotes)
        {
            Destroy(sn);
        }
    }

    //private void OnDisable()
    //{
    //    GameObject[] songNotes = GameObject.FindGameObjectsWithTag("SongNote");
    //    foreach (GameObject child in songNotes)
    //    {
    //        Destroy(child);
    //    }
    //}

    #endregion // PRIVATE_METHODS
}
