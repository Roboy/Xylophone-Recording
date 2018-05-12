using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class KeyAudioFeedback : MonoBehaviour {

    private AudioSource keySound;

    // Use this for initialization
    void Start () {
        keySound = GetComponent<AudioSource>();
	}
	
	

    public void PlayKey()
    {
        keySound.Play();
        //Debug.Log("play key!");
    }
}
