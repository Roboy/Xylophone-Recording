using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridge;
using ROSBridgeCustom;
using RtMidi.Core.Devices;
using RtMidi.Core;
using RtMidi.Core.Enums;
using RtMidi.Core.Unmanaged;
using RtMidi.Core.Messages;
using Serilog;
using System.Linq;

//TODO: left for testing purposes - can be deactivated or deleted when the collision detection with sending ros messages works!
public class NotesManager : Singleton<NotesManager>
{
    #region PUBLIC_MEMBER_VARIABLES
    public float Period = 1f;
    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES
    private float m_NextActionTime = 0.0f;
    private readonly System.DateTime m_UnixEpoch =
                                      new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
    private IMidiOutputDevice outputDevice;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS
    private void Start()
    {
        //Debug.Log(MidiDeviceManager.Default.GetAvailableMidiApis().Count());
        RtMidi.Core.Devices.Infos.IMidiOutputDeviceInfo outputDeviceInfo = MidiDeviceManager.Default.OutputDevices.Skip(1).First();
        this.outputDevice = outputDeviceInfo.CreateDevice();
        Debug.Log(outputDeviceInfo.Name);
        this.outputDevice.Open();
    }

    void Update()
    {
        if (Time.time > m_NextActionTime)
        {
            m_NextActionTime += Period;
            //sendROSMessage();
            NoteOffMessage msg = new NoteOffMessage(Channel.Channel1, Key.Key12, 12);
            Debug.Log(msg);
            this.outputDevice.Send(msg);
        }
    }

    private void OnApplicationQuit()
    {
        this.outputDevice.Close();
    }
    #endregion // MONOBEHAVIOR_METHODS

    #region PUBLIC_METHODS
    public System.Byte RandomMusicalNote()
    {
        System.Byte musicalNote;
        List<System.Byte> musicalNotes = new List<System.Byte>();
        musicalNotes.Add(12);
        musicalNotes.Add(14);
        musicalNotes.Add(16);

        musicalNote = musicalNotes[Random.Range(0, musicalNotes.Count)];
        return musicalNote;
    }
    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS
    private void sendROSMessage()
    {
        MusicalNoteMsg msg;
        System.Byte randomMusicalNote = RandomMusicalNote();
        long absoluteTime = getCurrentUnixTimestampMillis();

        msg = new MusicalNoteMsg(randomMusicalNote, absoluteTime);
        ROSBridge.ROSBridge.Instance.Publish(MusicalNotesPublisher.GetMessageTopic(), msg);
    }

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
