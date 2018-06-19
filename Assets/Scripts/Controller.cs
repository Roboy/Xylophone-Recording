using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Controller : MonoBehaviour
{
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
    private bool isSongBoardActive = false;
    private bool isApplicationButtomPressed = false;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        makeControllerDisable();
        makeGameBoardDisableAtStart();
        makeCubeStickInvis();
        makeRoboyStickInvis();
        makeRoboyStickInvis();
    }

    private void makeControllerDisable()
    {
        GameObject controllerMenu = GameObject.Find("ControllerMenu");
        makeGameObjectActive("ControllerMenu", false);
        controllerMenu.transform.localScale = new Vector3(1,1,1);
    }

    private void makeGameBoardDisableAtStart()
    {
        GameObject controllerMenu = GameObject.FindGameObjectWithTag("SongBoard");
        makeGameObjectActive("SongBoard", false);
        controllerMenu.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
    }

    void Update()
    {
        if (trackedObject == null)
        {
            return;
        }
        device = SteamVR_Controller.Input((int)trackedObject.index);

        CheckAndShowSongBoard();

        CheckAndHideSongBoard();

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            //Show the choosing menu
            gripButtonPressed = true;
            makeGameObjectActive("ControllerMenu", true);
        }
        if (gripButtonPressed && device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //if they do not touche the touchPad just remove the menu
            touchPadTouched = true;
            gripButtonPressed = false;
        }
        if (touchPadTouched && !device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            touchPadTouched = false;
            makeControllerDisable();
        }

        if (touchPadTouched /*device.GetAxis().x != 0*/)
        {

            if (device.GetAxis().x < -0.33f)
            {
                makeFirstMenuNotGlow();
                makeSecondMenuNotGlow();
                makeThirdMenuGlow();
                makeLightSaberStickVisible();
                makeCubeStickInvis();
                makeRoboyStickInvis();
            }
            else if (device.GetAxis().x > -0.33f && device.GetAxis().x < 0.33f)
            {
                makeSecondMenuGlow();
                makeFirstMenuNotGlow();
                makeThirdMenuNotGlow();
                makeCubeStickInvis();
                makeRoboyStickVisible();
                makeLightSaberStickInvis();
            }
            else if (device.GetAxis().x > 0.33f)
            {
                makeSecondMenuNotGlow();
                makeFirstMenuGlow();
                makeThirdMenuNotGlow();
                makeCubeStickVisible();
                makeRoboyStickInvis();
                makeLightSaberStickInvis();
            }
        }

    }

    private void CheckAndShowSongBoard()
    {
        if (!isSongBoardActive && isApplicationButtomPressed && device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            isSongBoardActive = true;
            isApplicationButtomPressed = false;
        }

        if (!isSongBoardActive && device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            makeGameObjectActive("SongBoard", true);
            isApplicationButtomPressed = true;
        }
    }

    private void CheckAndHideSongBoard()
    {
        if (isSongBoardActive && isApplicationButtomPressed && device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            isSongBoardActive = false;
            isApplicationButtomPressed = false;
        }

        if (isSongBoardActive && device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            makeGameObjectActive("SongBoard", false);
            isApplicationButtomPressed = true;
        }
    }

    private void makeThirdMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu2");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void makeSecondMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu1");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void makeFirstMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu0");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void makeThirdMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu2");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void makeSecondMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu1");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void makeFirstMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu0");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void makeLightSaberStickVisible()
    {
        makeGameObjectActive("lightsaberStick", true);

    }

    private void makeLightSaberStickInvis()
    {
        makeGameObjectActive("lightsaberStick", false);
    }

    private void makeCubeStickVisible()
    {
        makeGameObjectActive("cubeStick", true);
    }

    private void makeCubeStickInvis()
    {
        makeGameObjectActive("cubeStick", false);
    }

    private void makeRoboyStickVisible() { 

        makeGameObjectActive("roboyStick", true);
    }

    private void makeRoboyStickInvis()
    {
        makeGameObjectActive("roboyStick", false);
    }

    private void makeGameObjectActive(string objectTagName, bool active)
    {
        if (active) {
            Util.enableObject(objectTagName);
        }
        else {
            Util.disableObject(objectTagName);
        }
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