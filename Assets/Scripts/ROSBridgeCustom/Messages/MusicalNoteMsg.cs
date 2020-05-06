using SimpleJSON;
using System;
using ROSBridge;

namespace ROSBridgeCustom
{
    /// <summary>
    /// ROSBridge to ROS message interface for the musicalNotes messages
    /// </summary>
    public class MusicalNoteMsg : ROSBridgeMsg
    {
        #region PRIVATE_MEMBER_VARIABLES
        //Midi Note Format -> http://www.music.mcgill.ca/~ich/classes/mumt306/StandardMIDIfileformat.html#BMA1_3
        private Byte m_MusicialNote;

        private long m_UnixTimeInMilliseconds;
        #endregion // PRIVATE_MEMBER_VARIABLES

        #region PUBLIC_METHODS
        public MusicalNoteMsg(JSONNode msg)
        {
            throw new System.NotImplementedException();
        }


        public MusicalNoteMsg(Byte musicalNote, long unixTimeInMilliseconds)
        {
            m_MusicialNote = musicalNote;
            m_UnixTimeInMilliseconds = unixTimeInMilliseconds;
        }

        public static string GetMessageType()
        {
            return "roboy_communication_control/MusicalNote";
        }


        public override string ToString()
        {
            throw new System.NotImplementedException();
        }

        public override string ToYAMLString()
        {
            return "{" + "\"musicalNote\" : " + m_MusicialNote + ", \"unixTimeInMilliseconds\" : " + m_UnixTimeInMilliseconds + "}";
        }
        #endregion // PUBLIC_METHODS
    }
}
