using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridge;
using ROSBridgeCustom;

namespace XylophoneHero
{
    /// <summary>
    /// stores the note data and populates the communication functions
    /// </summary>
    public class MusicalNote : MonoBehaviour
    {
        #region PUBLIC_MEMBER_VARIABLES
        //Standard is E5 which is 76 in MIDI Format
        public int MusicalMIDINote = 76;
        #endregion // PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES
        private readonly System.DateTime m_UnixEpoch =
                                          new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        #endregion // PRIVATE_MEMBER_VARIABLES

        #region PUBLIC_METHODS
        public void PublishMusicalNoteViaROS()
        {
            MusicalNoteMsg msg;
            long absoluteTime = getCurrentUnixTimestampMillis();

            msg = new MusicalNoteMsg(System.Convert.ToByte(MusicalMIDINote), absoluteTime);
            ROSBridge.ROSBridge.Instance.Publish(MusicalNotesPublisher.GetMessageTopic(), msg);
        }

        public void SendNoteOnMessage()
        {
            MidiBridge.Instance.NoteOnMessage(MusicalMIDINote, 127);
        }

        public void SendNoteOffMessage()
        {
            MidiBridge.Instance.NoteOffMessage(MusicalMIDINote, 127);
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private long getCurrentUnixTimestampMillis()
        {
            return (long)(System.DateTime.UtcNow - m_UnixEpoch).TotalMilliseconds;
        }

        private System.DateTime dateTimeFromUnixTimestampMillis(long millis)
        {
            return m_UnixEpoch.AddMilliseconds(millis);
        }

        private long getCurrentUnixTimestampSeconds()
        {
            return (long)(System.DateTime.UtcNow - m_UnixEpoch).TotalSeconds;
        }

        private System.DateTime dateTimeFromUnixTimestampSeconds(long seconds)
        {
            return m_UnixEpoch.AddSeconds(seconds);
        }
        #endregion // PRIVATE_METHODS
    }
}
