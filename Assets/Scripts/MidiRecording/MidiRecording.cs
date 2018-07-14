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
    /// TODO: Fillout
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

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOR_METHODS
        private void Start()
        {
            m_MidiFile = new MidiFile();
            m_TempoMap = m_MidiFile.GetTempoMap();
            TrackChunk trackChunk = new TrackChunk();
            trackChunk.Events.Add(new SetTempoEvent(500000));
            m_TimedEventsManager = new TimedEventsManager(trackChunk.Events);
            m_TimedEventsCollection = m_TimedEventsManager.Events;
            m_StartingTime = System.DateTime.UtcNow;
        }

        private void OnApplicationQuit()
        {
            m_MidiFile.Chunks.Add(TimedEventsManagingUtilities.ToTrackChunk(m_TimedEventsCollection));
            m_TimedEventsManager.SaveChanges();
            m_MidiFile.Write(midiFilePath, overwriteFile: overwriteFile);
        }
        #endregion // MONOBEHAVIOR_METHODS

        #region PUBLIC_METHODS

        public void NoteOnMessage(byte key, byte velocity)
        {
            System.DateTime TimeNow = System.DateTime.UtcNow;
            System.TimeSpan TimeDifferenceSinceStart = TimeNow - m_StartingTime;
            TimedEventsManagingUtilities.AddEvent(m_TimedEventsCollection, new NoteOnEvent((SevenBitNumber)key, (SevenBitNumber)velocity), new MetricTimeSpan(TimeDifferenceSinceStart), m_TempoMap);
        }

        public void NoteOffMessage(byte key, byte velocity)
        {
            System.DateTime TimeNow = System.DateTime.UtcNow;
            System.TimeSpan TimeDifferenceSinceStart = TimeNow - m_StartingTime;
            TimedEventsManagingUtilities.AddEvent(m_TimedEventsCollection, new NoteOffEvent((SevenBitNumber)key, (SevenBitNumber)velocity), new MetricTimeSpan(TimeDifferenceSinceStart), m_TempoMap);
        }

        #endregion // PUBLIC_METHODS
    }
}
