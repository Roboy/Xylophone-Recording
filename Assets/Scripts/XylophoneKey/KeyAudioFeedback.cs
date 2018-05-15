using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class KeyAudioFeedback : MonoBehaviour {

    private AudioSource keySound;

    private float maxForce = 1000;

    // Use this for initialization
    void Start () {
        keySound = GetComponent<AudioSource>();
	}
	
	

    public void PlayKey(float force)
    {
        float vol = Mathf.Clamp(force / maxForce, 0.0f, 1.0f);
        keySound.volume = vol;
        keySound.Play();
    }
}
