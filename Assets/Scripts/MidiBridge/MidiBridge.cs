using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RtMidi.Core;
using RtMidi.Core.Devices;
using RtMidi.Core.Enums;
using RtMidi.Core.Unmanaged;
using RtMidi.Core.Messages;
//Serilog is a dependency of RtMidi
using Serilog;
using System.Linq;
using ROSBridge;

namespace XylophoneHero
{
    public class MidiBridge : Singleton<MidiBridge>
    {
        #region PUBLIC_MEMBER_VARIABLES
        //starts with 1 as at least on Windows the MidiDevice Numbers start with 1 in the GUIs
        public int MidiDeviveNumber = 1;
        public Channel MidiChannel = Channel.Channel1;
        #endregion // PUBLIC_MEMBER_VARIABLES

        #region PRIVATE_MEMBER_VARIABLES
        private IMidiOutputDevice m_OutputDevice;
        #endregion // PRIVATE_MEMBER_VARIABLES

        #region MONOBEHAVIOR_METHODS
        private void Start()
        {
            Debug.Log("# of vailable MIDI APIs: " + MidiDeviceManager.Default.GetAvailableMidiApis().Count());
            Debug.Log("Available MIDI APIs: " + MidiDeviceManager.Default.GetAvailableMidiApis());
            try
            {
                IEnumerable<RtMidi.Core.Devices.Infos.IMidiOutputDeviceInfo> outputDevices = MidiDeviceManager.Default.OutputDevices;
                RtMidi.Core.Devices.Infos.IMidiOutputDeviceInfo outputDeviceInfo = outputDevices.ElementAt(MidiDeviveNumber - 1);
                this.m_OutputDevice = outputDeviceInfo.CreateDevice();
                Debug.Log("Now using MidiDevice Number: " + MidiDeviveNumber + outputDeviceInfo.Name);
                this.m_OutputDevice.Open();
            }
            catch (System.Exception e)
            {
                Debug.Log("Apparently the MidiDeviceNumber seems to be not available");
                Debug.LogException(e);
            }

        }

        private void OnApplicationQuit()
        {
            this.m_OutputDevice.Close();
        }
        #endregion // MONOBEHAVIOR_METHODS

        #region PUBLIC_METHODS
        public void NoteOnMessage(int key, int velocity)
        {
            try
            {
                NoteOnMessage msg = new NoteOnMessage(MidiChannel, (Key)key, velocity);
                Debug.Log("SENT Midi msg: " + msg);
                this.m_OutputDevice.Send(msg);
            }
            catch (System.Exception e)
            {
                Debug.Log("Educated guess there was a faulty int->key cast");
                Debug.LogException(e);
            }
        }

        public void NoteOffMessage(int key, int velocity)
        {
            try
            {
                NoteOffMessage msg = new NoteOffMessage(MidiChannel, (Key)key, velocity);
                Debug.Log("SENT Midi msg: " + msg);
                this.m_OutputDevice.Send(msg);
            }
            catch (System.Exception e)
            {
                Debug.Log("Educated guess there was a faulty int->key cast");
                Debug.LogException(e);
            }
        }
        #endregion // PUBLIC_METHODS
    }
}
