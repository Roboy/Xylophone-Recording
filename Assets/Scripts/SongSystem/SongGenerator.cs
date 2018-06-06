using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongGenerator : MonoBehaviour {

    #region PUBLIC_MEMBER_VARIABLES

    public string SongFilePath;

    //  the x coordinate of the corresponding note
    //  the program will generate the note at this position
    public float PositionC = 1.5f;
    public float PositionD = 1.0f;
    public float PositionE = 0.5f;
    public float PositionF = 0f;
    public float PositionG = -0.5f;
    public float PositionA = -1.0f;
    public float PositionB = -1.5f;

    public float Bpm = 60f;

    public bool SongStart = true;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES

    [SerializeField] private GameObject m_SongNotePrefab;

    private List<string> m_SongData;

    private bool m_LoadSongFinish = false;
    private bool m_Playing = false;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS

    private void Start()
    {
        loadSong();
    }

    private void Update()
    {
        if (SongStart && m_LoadSongFinish && !m_Playing)
        {
            Debug.Log("can play song");
            m_Playing = true;
            StartCoroutine(playSong());
        }
    }

    #endregion // MONOBEHAVIOR_METHODS

    #region PUBLIC_METHODS

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    private void loadSong()
    {
        m_SongData = new List<string>(File.ReadAllLines(SongFilePath));
        m_LoadSongFinish = true;
    }

    private IEnumerator playSong()
    {
        float noteInterval = 60 / Bpm;
        foreach(string line in m_SongData)
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
    
    #endregion // PRIVATE_METHODS
}
