using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using XylophoneHero.SongSystem.Utils;

public class SongGenerator : MonoBehaviour {

    #region PUBLIC_MEMBER_VARIABLES

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

    public SongManager Manager;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES

    [SerializeField] private GameObject m_SongNotePrefab;

    private bool m_IsGenerating = false;
    private Coroutine m_GenerationCoroutine = null;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS

    #endregion // MONOBEHAVIOR_METHODS

    #region PUBLIC_METHODS

    public void StartGenerate(Song song)
    {
        m_IsGenerating = true;
        m_GenerationCoroutine = StartCoroutine(generateNotes(song.Content));
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
            m_IsGenerating = false;
            Manager.SongFinish();
        }
    }

    public void GenerateNext()
    {

    }

    public void GeneratePrev()
    {

    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    private IEnumerator generateNotes(List<string> songData)
    {
        float noteInterval = 60 / Bpm;
        foreach(string line in songData)
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
        m_IsGenerating = false;
        Manager.SongFinish();
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

    #endregion // PRIVATE_METHODS
}
