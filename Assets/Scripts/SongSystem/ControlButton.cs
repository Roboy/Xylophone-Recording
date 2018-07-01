using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButton : MonoBehaviour {

    public enum Control
    {
        SongStart,
        SongPause,
        SongStop,
        SongPrev,
        SongNext
    }

    #region PUBLIC_MEMBER_VARIABLES

    public Control ControlType;
    public SongManager Manager;
    public KeyCode TestKeyCode;

    #endregion // PUBLIC_MEMEBER_VARIABLES

    #region MONOBEHAVIOUR_METHODS

    private void Update()
    {
        if (Input.GetKeyDown(TestKeyCode))
        {
            buttonAction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        buttonAction();
    }

    #endregion // MONOBEHAVIOUR_METHODS

    #region PRIVATE_METHODS

    private void buttonAction()
    {
        switch (ControlType)
        {
            case Control.SongStart:
                Manager.StartSong();
                break;
            case Control.SongPause:
                Manager.PauseSong();
                break;
            case Control.SongStop:
                Manager.StopSong();
                break;
            case Control.SongPrev:
                Manager.PrevSong();
                break;
            case Control.SongNext:
                Manager.NextSong();
                break;
            default:
                break;
        }
    }

    #endregion // PRIVATE_METHODS

}
