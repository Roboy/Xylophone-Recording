using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XylophoneHero
{
    public class ToggleRecording : MonoBehaviour
    {
        public KeyCode TestKeyCode;

        private void Update()
        {
            if (Input.GetKeyDown(TestKeyCode))
            {
                MidiRecording.Instance.ToggleRecording();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            MidiRecording.Instance.ToggleRecording();
        }
    }
}