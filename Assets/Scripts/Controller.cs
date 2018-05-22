using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Controller : MonoBehaviour
{
    //Notes color which is green
    public Color greenColor = Color.green;
    //xylophone color which is red
    public Color redColor = Color.red;
    public delegate void OnTrackpadPress(int deviceID, string side);
    public static OnTrackpadPress TrackpadPressed;

    public delegate void OnTriggerPress();
    public static OnTriggerPress TriggerPressed;

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private List<Rigidbody> keyRigidbodies = new List<Rigidbody>();
    private Image roboy_image;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        for (int i = 0; i < keys.Length; i++)
        {
            keyRigidbodies.Add(keys[i].GetComponent<Rigidbody>());
        }
        makeCubeStickVisible();
        makeRoboyStickInvis();
        makeLightSaberStickInvis();
    }

    void Update()
    {
        if (trackedObject == null)
        {
            return;
        }
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if (device.GetAxis().x != 0)
        {
            if (device.GetAxis().x < -0.33f)
            {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    makeCubeStickVisible();
                    makeRoboyStickInvis();
                    makeLightSaberStickInvis();
                }
            }
            else if (device.GetAxis().x > -0.33f && device.GetAxis().x < 0.33f)
            {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    makeCubeStickInvis();
                    makeRoboyStickVisible();
                    makeLightSaberStickInvis();

                }
            }
            else if (device.GetAxis().x > 0.33f)
            {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    makeLightSaberStickVisible();
                    makeCubeStickInvis();
                    makeRoboyStickInvis();
                }
            }
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            TriggerPressed();
        }
    }

    private void makeLightSaberStickVisible()
    {
        changeGameObjectSize("Right_stick_lightsaber", 1F, 1F, 1F);
        changeGameObjectSize("Left_stick_lightsaber", 1F, 1F, 1F);
    }

    private void makeLightSaberStickInvis()
    {
        changeGameObjectSize("Right_stick_lightsaber", 0F, 0F, 0F);
        changeGameObjectSize("Left_stick_lightsaber", 0F, 0F, 0F);
    }

    private void makeCubeStickVisible()
    {
        changeGameObjectSize("Right_stick_cube", 1F, 1F, 1F);
        changeGameObjectSize("Left_stick_cube", 1F, 1F, 1F);
    }

    private void makeCubeStickInvis()
    {
        changeGameObjectSize("Right_stick_cube", 0F, 0F, 0F);
        changeGameObjectSize("Left_stick_cube", 0F, 0F, 0F);
    }

    private void makeRoboyStickVisible()
    {
        /*changeGameObjectSize("Right_stick_roboy", 0.05F, 0.1F, 0.1F);
        changeGameObjectSize("Left_stick_roboy", 0.05F, 0.1F, 0.1F);*/

        changeGameObjectSize("Right_stick_roboy", 1F, 1F, 1F);
        changeGameObjectSize("Left_stick_roboy", 1F, 1F, 1F);
    }

    private void makeRoboyStickInvis()
    {
        changeGameObjectSize("Right_stick_roboy", 0F, 0F, 0F);
        changeGameObjectSize("Left_stick_roboy", 0F, 0F, 0F);
    }

    /*private void changeSticks(string headObject, string stickObject, float headSize, float stickSize)
    {
        GameObject stick = GameObject.Find(stickObject);
        stick.transform.localScale = new Vector3(0.01F, 0F, stickSize);
        GameObject stickHead = GameObject.Find(headObject);
        stickHead.transform.localScale = new Vector3(headSize, headSize, headSize);
        stickHead.transform.localPosition = new Vector3(0, 0, stickSize/2);
    }
    */
    private void changeGameObjectSize(string objectName, float sizex, float sizey, float sizez)
    {
        GameObject stick = GameObject.Find(objectName);
        stick.transform.localScale = new Vector3(sizex, sizey, sizez);
    }

    void onTriggerStay(Collider col)
    {
        Debug.Log("You have collided with " + col.name + " and activated onTriggerStay");
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with " + col.name + " while holding down Touch");
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.transform.SetParent(gameObject.transform);
        }
    }
}