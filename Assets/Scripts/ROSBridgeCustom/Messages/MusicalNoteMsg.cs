using SimpleJSON;
using System;
using ROSBridge;

namespace ROSBridgeCustom
{
    public class MusicalNoteMsg : ROSBridgeMsg
    {

        private string _musicialNote;

        private System.DateTime _absoluteTime;

        public MusicalNoteMsg(JSONNode msg)
        {
            throw new System.NotImplementedException();
        }


        public MusicalNoteMsg(string musicalNote, System.DateTime absoluteTime)
        {
            _musicialNote = musicalNote;
            _absoluteTime = absoluteTime;
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
            return "{" + "\"musicalNote\" : \"" + _musicialNote + "\", \"absoluteTime\" : \"" + _absoluteTime.ToString() + "\"}";
        }
    }
}
