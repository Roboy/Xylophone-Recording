using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongKeyIndicate : MonoBehaviour {

    #region PUBLIC_MEMBER_VARIABLES
    public KeyCode TestKeyCode;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES
    private GameObject songNoteObject = null;
    private Renderer rend;
    private Color originColor;
    private Color highlightColor = new Color(1.0f, 1.0f, 1.0f);
    private const float lerp = 0.6f;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS
    void Update()
    {
        if(Input.GetKeyDown(TestKeyCode))
        {
            HandleStrike();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SongNote")
        {
            songNoteObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == songNoteObject)
        {
            songNoteObject = null;
        }
    }
    #endregion // MONOBEHAVIOR_METHODS

    #region PUBLIC_METHODS
    public void HandleStrike()
    {
        bool strikeResult = DestroySongNote();
        if (strikeResult)
        {

        }
        else
        {

        }
    }

    public bool DestroySongNote()
    {
        if(songNoteObject == null)
        {
            return false;
        }
        Destroy(songNoteObject);
        return true;
    }
    #endregion // PUBLIC_METHODS
}
