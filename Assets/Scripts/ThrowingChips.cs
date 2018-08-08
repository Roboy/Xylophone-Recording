using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingChips : MonoBehaviour {

    private SteamVR_Controller.Device device;
    private SteamVR_TrackedObject trackedObject;
    private int previousPosition=0;
    // Use this for initialization
    void Start () {

        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject sticker = GameObject.FindGameObjectWithTag(TagsConstants.CUBE_STICK);
        if (sticker != null)
        if (previousPosition - sticker.transform.position.z > 0.1) {
                GameObject chips = GameObject.FindGameObjectWithTag("ThrowedChips");
                GameObject Chips1 = Instantiate(chips, new Vector3(0,0,0), chips.transform.rotation) as GameObject;
            }
        }
    
}
