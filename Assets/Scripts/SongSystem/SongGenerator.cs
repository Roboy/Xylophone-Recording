using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongGenerator : MonoBehaviour {

    #region PUBLIC_MEMBER_VARIABLES

    public string SongFolderPath;

    //  the x coordinate of the corresponding note
    //  the program will generate the note at this position
    public float PositionC;
    public float PositionD;
    public float PositionE;
    public float PositionF;
    public float PositionG;
    public float PositionA;
    public float PositionB;

    public float Bpm;

    public bool SongStart;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES

    [SerializeField] private GameObject m_SongNotePrefab;

    private List<List<string>> m_SongDataList;

    private bool m_LoadSongFinish;
    private bool m_Playing;

    private int m_CurrentSongIndex;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS

    private SongGenerator() {
        InitVariables();
    }

    private void InitVariables() {
        PositionC = 1.5f;
        PositionD = 1.0f;
        PositionE = 0.5f;
        PositionF = 0f;
        PositionG = -0.5f;
        PositionA = -1.0f;
        PositionB = -1.5f;
        Bpm = 60f;
        SongStart = true;
        m_LoadSongFinish = false;
        m_Playing = false;
        m_CurrentSongIndex = -1;

}

    private void OnEnable()
    {
        InitVariables();
        Start();
    }

    private void Start()
    {
        m_SongDataList = new List<List<string>>();
        loadSong();
    }

    private void Update()
    {
        if (SongStart && m_LoadSongFinish && !m_Playing)
        {
            m_Playing = true;
            StartCoroutine(playSong());
        }
    }

    #endregion // MONOBEHAVIOR_METHODS

    #region PUBLIC_METHODS

    public void StartSong()
    {
    }

    public void PauseSong()
    {

    }

    public void StopSong()
    {

    }

    public void PlayNextSong()
    {

    }

    public void PlayPrevSong()
    {

    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    private void loadSong()
    {
        string[] fileNameArray = System.IO.Directory.GetFiles(SongFolderPath);
        foreach (string filename in fileNameArray)
        {
            if (filename.EndsWith("txt"))
            {
                List<string> songData = new List<string>(File.ReadAllLines(filename));
                m_SongDataList.Add(songData);
            }
        }
        m_LoadSongFinish = true;
    }
    
    private IEnumerator playSong()
    {
        float noteInterval = 60 / Bpm;
        foreach(string line in m_SongDataList[0])
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

    private void OnDisable()
    {
        GameObject[] songNotes = GameObject.FindGameObjectsWithTag("SongNote");
        foreach (GameObject child in songNotes)
        {
            Destroy(child);
        }
    }

    #endregion // PRIVATE_METHODS
}
