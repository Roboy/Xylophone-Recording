﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAudioFeedback : MonoBehaviour {

    private AudioSource keySound;

    // Use this for initialization
    void Start () {
        keySound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	

    public void PlayKey()
    {
        keySound.Play();
        //Debug.Log("play key!");
    }
}