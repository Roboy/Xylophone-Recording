using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class KeyAudioFeedback : MonoBehaviour {

    #region PRIVATE_MEMBER_VARIABLES

    private AudioSource keySound;
    private float maxForce = 500f;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOR_METHODS

    void Start () {
        keySound = GetComponent<AudioSource>();
	}

    #endregion // MONOBEHAVIOR_METHOD

    #region PUBLIC_METHODS

    public void PlayKey(float force)
    {
        float vol = Mathf.Clamp(force / maxForce, 0.0f, 1.0f);
        keySound.volume = vol;
        keySound.Play();
    }

    #endregion // PUBLIC_METHODS
}
