using ROSBridge;

namespace ROSBridgeCustom
{
    public class MusicalNotesPublisher : ROSBridgePublisher
    {
        #region PUBLIC_METHODS
        public new static string GetMessageTopic()
        {
            return "/roboy/control/musicalNote";
        }

        public new static string GetMessageType()
        {
            return MusicalNoteMsg.GetMessageType();
        }

        public static string ToYAMLString(MusicalNoteMsg msg)
        {
            return msg.ToYAMLString();
        }
        #endregion // PUBLIC_METHODS
    }
}
