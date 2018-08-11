using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using XylophoneHero.SongSystem.Utils;

namespace XylophoneHero.SongSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class SongGenerator : MonoBehaviour
    {

        #region PUBLIC_MEMBER_VARIABLES

        //  the x coordinate of the corresponding note
        //  the program will generate the note at this position

        public float[] Positions = new float[] { 1.98f,
                                    1.62f,
                                    1.26f,
                                    0.90f,
                                    0.54f,
                                    0.18f,
                                    -0.18f,
                                    -0.54f,
                                    -0.90f,
                                    -1.26f,
                                    -1.62f,
                                    -1.98f };

        public float Bpm = 120f;

        #endregion // PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES

        [SerializeField] private GameObject m_SongNotePrefab;

        private bool m_IsGenerating = false;
        private Coroutine m_GenerationCoroutine = null;

        #endregion // PRIVATE_MEMBER_VARIABLES

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
            if (m_GenerationCoroutine != null)
            {
                killAllNotes();
                StopCoroutine(m_GenerationCoroutine);
                m_IsGenerating = false;
                SongManager.Instance.SongFinish();
            }
        }

        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS

        private IEnumerator generateNotes(List<string> songData)
        {
            float noteInterval = 60 / Bpm;

            foreach(string line in songData)
            {
                int l = line.Length;
                for (int idx = 0; idx < l; idx++)
                {
                    if(line[idx] == '1')
                    {
                        generateNote(Positions[idx]);
                    }
                }
                yield return new WaitForSeconds(noteInterval);
            }

            m_IsGenerating = false;
            SongManager.Instance.SongFinish();
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

}
