using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongManager : MonoBehaviour {


    #region PUBLIC_MEMBER_VARIABLES
    public float PrompShowTime = 0.5f;
    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES

    private GameObject m_InfoPromps;
    private TextMeshPro m_InfoPrompsText;
    private GameObject m_ScoreDisplay;
    private TextMeshPro m_ScoreDisplayText;

    private int m_ComboCounter = 0;
    private int m_Score = 0;
    private string m_Promps = "";

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS

    #endregion // MONOBEHAVIOR_METHODS

    private void Start()
    {
        
        m_InfoPromps = transform.Find("InfoPromps").gameObject;
        m_ScoreDisplay = transform.Find("ScoreDisplay").gameObject;

        if(m_InfoPromps != null && m_ScoreDisplay != null)
        {
            m_InfoPrompsText = m_InfoPromps.GetComponent<TextMeshPro>();
            m_ScoreDisplayText = m_ScoreDisplay.GetComponent<TextMeshPro>();
        }
        
    }
    
    #region PUBLIC_METHODS
    
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
        StartCoroutine(DisplayScoreText());
    }

    public void BadHit()
    {
        m_ComboCounter = 0;
        m_InfoPromps.SetActive(false); 
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS
    
    private IEnumerator DisplayScoreText()
    {
        m_InfoPromps.SetActive(false);

        m_ScoreDisplayText.SetText("Score: " + m_Score);
        m_InfoPrompsText.SetText(m_Promps);

        m_InfoPromps.SetActive(true);

        yield return new WaitForSeconds(PrompShowTime);

        if (m_InfoPromps.activeSelf)
        {
            m_InfoPromps.SetActive(false);
        }
    }

    private void OnEnable()
    {
        m_Score = 0;
        m_InfoPromps.SetActive(false);
        m_ScoreDisplayText.SetText("Score: " + m_Score);
    }

    #endregion // PRIVATE_METHODS

}