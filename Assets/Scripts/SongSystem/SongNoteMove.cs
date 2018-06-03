using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongNoteMove : MonoBehaviour {

    #region PUBLIC_MEMBER_VARIABLES
    public float speed = 2;
    #endregion


    #region MONOBEHAVIOR_METHODS
    void Start () {
		
	}
	
	void Update () {
        transform.Translate(new Vector3(0, -speed*Time.deltaTime, 0));
	}
    #endregion // MONOBEHAVIOR_METHODS
}
