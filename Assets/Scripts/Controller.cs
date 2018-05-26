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
    private bool gripButtonPressed = false;
    private bool touchPadTouched = false;
    Color whiteColor = new Color32(255, 255, 255, 255);
    Color greyColor = new Color32(173, 173, 173, 255);
    public Rigidbody attachPoint;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        //GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        //for (int i = 0; i < keys.Length; i++)
        /*{
            keyRigidbodies.Add(keys[i].GetComponent<Rigidbody>());
        }

        GameObject stick = GameObject.Find("Left_stick_lightsaber");
        FixedJoint joint = stick.AddComponent<FixedJoint>();
        joint.connectedBody = attachPoint;*/

        makeCubeStickInvis();
        makeRoboyStickInvis();
        makeLightSaberStickVisible();
    }

    void Update()
    {
        if (trackedObject == null)
        {
            return;
        }
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            //Show the choosing menu
            gripButtonPressed = true;
            makeGameObjectActive("ControllerMenu", 1,1,1);
        }
        if (gripButtonPressed && device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //if they do not touche the touchPad just remove the menu
            touchPadTouched = true;
            gripButtonPressed = false;
        }
        if (touchPadTouched && !device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) {
            touchPadTouched = false;
            makeGameObjectActive("ControllerMenu", 0,0,0);
        }

        if (touchPadTouched /*device.GetAxis().x != 0*/)
        {

            if (device.GetAxis().x < -0.33f)
            {
                //makeThirdMenuGlow();
                //makeSecondMenuNotGlow();
                //makeFirstMenuNotGlow();
                makeLightSaberStickVisible();
                makeCubeStickInvis();
                makeRoboyStickInvis();
            }
            else if (device.GetAxis().x > -0.33f && device.GetAxis().x < 0.33f)
            {
                //makeFirstMenuGlow();
                //makeSecondMenuNotGlow();
                //makeThirdMenuNotGlow();
                makeCubeStickInvis();
                makeRoboyStickVisible();
                makeLightSaberStickInvis();
            }
            else if (device.GetAxis().x > 0.33f)
            {
                //makeSecondMenuGlow();
                //makeFirstMenuNotGlow();
                //makeThirdMenuNotGlow();
                //}
                makeCubeStickVisible();
                makeRoboyStickInvis();
                makeLightSaberStickInvis();
            }
        }

    }

    private void makeThirdMenuGlow()
    {
        GameObject menu0 = GameObject.Find("SelectionWheelPart 0");
        Renderer menu0Render = menu0.GetComponent<Renderer>();
        menu0Render.material.SetColor("_Color", whiteColor);
    }

    private void makeSecondMenuGlow()
    {
        GameObject menu0 = GameObject.Find("SelectionWheelPart 2");
        Renderer menu0Render = menu0.GetComponent<Renderer>();
        menu0Render.material.SetColor("_Color", whiteColor);
    }

    private void makeFirstMenuGlow()
    {
        GameObject menu0 = GameObject.Find("SelectionWheelPart 3");
        Renderer menu0Render = menu0.GetComponent<Renderer>();
        menu0Render.material.SetColor("_Color", whiteColor);
    }

    private void makeThirdMenuNotGlow()
    {
        GameObject menu0 = GameObject.Find("SelectionWheelPart 0");
        Renderer menu0Render = menu0.GetComponent<Renderer>();
        menu0Render.material.SetColor("_Color", greyColor);
    }

    private void makeSecondMenuNotGlow()
    {
        GameObject menu0 = GameObject.Find("SelectionWheelPart 2");
        Renderer menu0Render = menu0.GetComponent<Renderer>();
        menu0Render.material.SetColor("_Color", greyColor);
    }

    private void makeFirstMenuNotGlow()
    {
        GameObject menu0 = GameObject.Find("SelectionWheelPart 3");
        Renderer menu0Render = menu0.GetComponent<Renderer>();
        menu0Render.material.SetColor("_Color", greyColor);
    }

    private void makeLightSaberStickVisible()
    {
        makeGameObjectActive("Right_stick_lightsaber", 1,1,1);
        makeGameObjectActive("Left_stick_lightsaber", 1,1,1);

    }

    private void makeLightSaberStickInvis()
    {
        makeGameObjectActive("Right_stick_lightsaber", 0,0,0);
        makeGameObjectActive("Left_stick_lightsaber", 0,0,0);
    }

    private void makeCubeStickVisible()
    {
        makeGameObjectActive("Right_stick_cube", 1,1,1);
        makeGameObjectActive("Left_stick_cube", 1,1,1);
    }

    private void makeCubeStickInvis()
    {
        makeGameObjectActive("Right_stick_cube", 0,0,0);
        makeGameObjectActive("Left_stick_cube",0,0,0);
    }

    private void makeRoboyStickVisible() { 

        makeGameObjectActive("Right_stick_roboy", 1,1,1);
        makeGameObjectActive("Left_stick_roboy", 1,1,1);
    }

    private void makeRoboyStickInvis()
    {
        makeGameObjectActive("Right_stick_roboy",0,0,0);
        makeGameObjectActive("Left_stick_roboy", 0,0,0);
    }

    private void makeGameObjectActive(string objectName, int sizeX, int sizeY, int sizeZ)
    {
        GameObject stick = GameObject.Find(objectName);
        stick.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
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