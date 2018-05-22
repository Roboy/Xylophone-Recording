using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStickers : MonoBehaviour {


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "stick_switch_button_right" 
            || col.gameObject.name == "stick_switch_button_left")
        {
            GameObject stick = GameObject.Find("stick_head_right");
            stick.transform.localScale = new Vector3(0.01F, 0F, 1F);
            stick = GameObject.Find("stick_head_left");
            stick.transform.localScale = new Vector3(0.01F, 0F, 1F);
        }
    }
}
