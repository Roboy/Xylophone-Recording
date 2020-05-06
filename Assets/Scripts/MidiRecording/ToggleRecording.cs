using UnityEngine;
using XylophoneHero.SongSystem.Utils;

namespace XylophoneHero
{
    public class ToggleRecording : DebounceButton
    {
        public KeyCode TestKeyCode;
        private ButtonAppearance m_Appearance;

        private void Start()
        {
            m_Appearance = GetComponent<ButtonAppearance>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(TestKeyCode))
            {
                bool RecordingStart = MidiRecording.Instance.ToggleRecording();
                m_Appearance.SwitchColor(RecordingStart);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_IsButtonActivated)
            {
                bool RecordingStart = MidiRecording.Instance.ToggleRecording();
                m_Appearance.SwitchColor(RecordingStart);

                debounce();
            }
        }
    }
}