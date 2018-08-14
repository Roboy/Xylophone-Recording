using UnityEngine;
using XylophoneHero.SongSystem.Utils;

namespace XylophoneHero
{
    public class ToggleRecording : MonoBehaviour
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
            bool RecordingStart = MidiRecording.Instance.ToggleRecording();
            m_Appearance.SwitchColor(RecordingStart);
        }
    }
}