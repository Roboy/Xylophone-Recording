using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero.SongSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlButton : MonoBehaviour
    {

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
                    SongManager.Instance.StartSong();
                    break;
                case Control.SongPause:
                    SongManager.Instance.PauseSong();
                    break;
                case Control.SongStop:
                    SongManager.Instance.StopSong();
                    break;
                case Control.SongPrev:
                    SongManager.Instance.PrevSong();
                    break;
                case Control.SongNext:
                    SongManager.Instance.NextSong();
                    break;
                default:
                    break;
            }
        }

        #endregion // PRIVATE_METHODS

    }

}
