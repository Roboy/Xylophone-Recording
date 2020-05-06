using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Smf.Interaction;
using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Common;
using ROSBridge;

namespace XylophoneHero
{
    /// <summary>
    /// Recording a Xylophone Session to a Midi File using the DryWetMidi library.
    /// </summary>
    public class MidiRecording : Singleton<MidiRecording>
    {
        #region PUBLIC_MEMBER_VARIABLES

        public string midiFilePath = "test.mid";
        public bool overwriteFile = false;

        #endregion // PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES

        private MidiFile m_MidiFile;
        private TimedEventsManager m_TimedEventsManager;
        private TimedEventsCollection m_TimedEventsCollection;
        private System.DateTime m_StartingTime;
        private TempoMap m_TempoMap;
        private bool m_RecordingActivated = false;
        private bool m_StartingRecording = false;
        private bool m_StoppingRecording = false;

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region PUBLIC_METHODS

        public bool ToggleRecording()
        {
            if (m_RecordingActivated)
            {
                StopMidiRecording();
                return false;
            }
            else
            {
                StartMidiRecording();
                return true;
            }
        }

        public void StartMidiRecording()
        {
            if(!m_StartingRecording)
            {
                m_StartingRecording = true;
                m_MidiFile = new MidiFile();
                m_TempoMap = m_MidiFile.GetTempoMap();
                TrackChunk trackChunk = new TrackChunk();
                trackChunk.Events.Add(new SetTempoEvent(500000));
                trackChunk.Events.Add(new SmpteOffsetEvent((SmpteFormat)24, 0, 0, 0, 0, 0));
                trackChunk.Events.Add(new TimeSignatureEvent());
                trackChunk.Events.Add(new SequenceNumberEvent(0));
                trackChunk.Events.Add(new SequenceTrackNameEvent("RoboyXylophoneVRRecording"));
                trackChunk.Events.Add(new MarkerEvent("First Marker"));
                m_TimedEventsManager = new TimedEventsManager(trackChunk.Events);
                m_TimedEventsCollection = m_TimedEventsManager.Events;
                m_StartingTime = System.DateTime.UtcNow;
                m_RecordingActivated = true;
                Debug.Log("StartRecording");
                m_StartingRecording = false;
            }
            else
            {
                Debug.LogError("Working on it! Already started a Recording Session.");
            }
            
        }

        public void StopMidiRecording()
        {
            if (!m_StartingRecording)
            {
                m_RecordingActivated = false;
                m_MidiFile.Chunks.Add(TimedEventsManagingUtilities.ToTrackChunk(m_TimedEventsCollection));
                m_TimedEventsManager.SaveChanges();
                m_MidiFile.Write(midiFilePath, overwriteFile: overwriteFile);
                Debug.Log("StopRecording");
            }
            else
            {
                Debug.LogError("Working on it! Already stopping a Recording Session.");
            }
        }

        public void NoteOnMessage(byte key, byte velocity)
        {
            if (m_RecordingActivated)
            {
                System.DateTime TimeNow = System.DateTime.UtcNow;
                System.TimeSpan TimeDifferenceSinceStart = TimeNow - m_StartingTime;
                TimedEventsManagingUtilities.AddEvent(m_TimedEventsCollection, new NoteOnEvent((SevenBitNumber)key, (SevenBitNumber)velocity), new MetricTimeSpan(TimeDifferenceSinceStart), m_TempoMap);
            }
        }

        public void NoteOffMessage(byte key, byte velocity)
        {
            if (m_RecordingActivated) {
                System.DateTime TimeNow = System.DateTime.UtcNow;
                System.TimeSpan TimeDifferenceSinceStart = TimeNow - m_StartingTime;
                TimedEventsManagingUtilities.AddEvent(m_TimedEventsCollection, new NoteOffEvent((SevenBitNumber)key, (SevenBitNumber)velocity), new MetricTimeSpan(TimeDifferenceSinceStart), m_TempoMap);
            }
        }
        #endregion // PUBLIC_METHODS
    }
}
