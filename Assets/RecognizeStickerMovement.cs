using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizeStickerMovement : MonoBehaviour {

    public GameObject sticker;
    private Vector3 stickerLastPosition;
    private float nextUpdate = 0.5F;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {

        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 0.5F;
            
            if (Mathf.Abs(stickerLastPosition.x - sticker.transform.position.x) +
            Mathf.Abs(stickerLastPosition.y - sticker.transform.position.y) + Mathf.Abs(stickerLastPosition.z - sticker.transform.position.z) > 0.01F)
            {
                if (sticker.name.Equals("Controller (right)")) Util.enableObject(TagsConstants.THROWED_CHIPS_RIGHT);
                else Util.enableObject(TagsConstants.THROWED_CHIPS_LEFT);
            }
            else
            {
                if (sticker.name.Equals("Controller (right)")) Util.disableObject(TagsConstants.THROWED_CHIPS_RIGHT);
                else Util.disableObject(TagsConstants.THROWED_CHIPS_LEFT);
            }
            stickerLastPosition = sticker.transform.localPosition;
        }


	}
}
