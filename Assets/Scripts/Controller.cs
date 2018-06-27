using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// Changing controller model is handled in this class.
/// </summary>
public class Controller : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES
    private delegate void OnTrackpadPress(int deviceID, string side);
    private static OnTrackpadPress TrackpadPressed;

    private delegate void OnTriggerPress();
    private static OnTriggerPress TriggerPressed;

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private List<Rigidbody> keyRigidbodies = new List<Rigidbody>();
    private Image roboy_image;
    private bool gripButtonPressed = false;
    private bool touchPadTouched = false;
    private Color whiteColor = new Color32(255, 255, 255, 255);
    private Color greyColor = new Color32(173, 173, 173, 255);
    public Rigidbody attachPoint;
    private bool isSongBoardActive = false;
    private bool isApplicationButtomPressed = false;
    #endregion // PRIVATE_MEMBER_VARIABLES

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        MakeControllerDisable();
        MakeGameBoardDisableAtStart();
        MakeCubeStickInvis();
        MakeRoboyStickInvis();
        MakeRoboyStickInvis();
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
            //Show the controller choosing menu
            gripButtonPressed = true;
            MakeGameObjectActive("ControllerMenu", true);
        }
        if (gripButtonPressed && device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //If user do not touche the touchPad just remove the menu
            touchPadTouched = true;
            gripButtonPressed = false;
        }
        if (touchPadTouched && !device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            touchPadTouched = false;
            MakeControllerDisable();
        }

        if (touchPadTouched)
        {

            if (device.GetAxis().x < -0.33f)
            {
                MakeFirstMenuNotGlow();
                MakeSecondMenuNotGlow();
                MakeThirdMenuGlow();
                MakeLightSaberStickVisible();
                MakeCubeStickInvis();
                MakeRoboyStickInvis();
            }
            else if (device.GetAxis().x > -0.33f && device.GetAxis().x < 0.33f)
            {
                MakeSecondMenuGlow();
                MakeFirstMenuNotGlow();
                MakeThirdMenuNotGlow();
                MakeCubeStickInvis();
                MakeRoboyStickVisible();
                MakeLightSaberStickInvis();
            }
            else if (device.GetAxis().x > 0.33f)
            {
                MakeSecondMenuNotGlow();
                MakeFirstMenuGlow();
                MakeThirdMenuNotGlow();
                MakeCubeStickVisible();
                MakeRoboyStickInvis();
                MakeLightSaberStickInvis();
            }
        }

    }

    #region PRIVATE_METHODS
    private void MakeControllerDisable()
    {
        GameObject controllerMenu = GameObject.Find("ControllerMenu");
        MakeGameObjectActive("ControllerMenu", false);
        controllerMenu.transform.localScale = new Vector3(1, 1, 1);
    }

    private void MakeGameBoardDisableAtStart()
    {
        GameObject controllerMenu = GameObject.FindGameObjectWithTag("SongBoard");
        MakeGameObjectActive("SongBoard", false);
        controllerMenu.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
    }

    private void CheckAndShowSongBoard()
    {
        if (!isSongBoardActive && isApplicationButtomPressed && device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            //Set parameter for hiding songBoard when user click on menu button next time.
            isSongBoardActive = true;
            isApplicationButtomPressed = false;
        }

        if (!isSongBoardActive && device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            MakeGameObjectActive("SongBoard", true);
            isApplicationButtomPressed = true;
        }
    }

    private void CheckAndHideSongBoard()
    {
        if (isSongBoardActive && isApplicationButtomPressed && device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            //Set parameter for showing songBoard when user click on menu button next time.
            isSongBoardActive = false;
            isApplicationButtomPressed = false;
        }

        if (isSongBoardActive && device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            MakeGameObjectActive("SongBoard", false);
            isApplicationButtomPressed = true;
        }
    }

    private void MakeThirdMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu2");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void MakeSecondMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu1");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void MakeFirstMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu0");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void MakeThirdMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu2");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void MakeSecondMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu1");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void MakeFirstMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag("SelectionMenu0");
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void MakeLightSaberStickVisible()
    {
        MakeGameObjectActive("lightsaberStick", true);

    }

    private void MakeLightSaberStickInvis()
    {
        MakeGameObjectActive("lightsaberStick", false);
    }

    private void MakeCubeStickVisible()
    {
        MakeGameObjectActive("cubeStick", true);
    }

    private void MakeCubeStickInvis()
    {
        MakeGameObjectActive("cubeStick", false);
    }

    private void MakeRoboyStickVisible()
    {

        MakeGameObjectActive("roboyStick", true);
    }

    private void MakeRoboyStickInvis()
    {
        MakeGameObjectActive("roboyStick", false);
    }

    private void MakeGameObjectActive(string objectTagName, bool active)
    {
        if (active)
        {
            Util.enableObject(objectTagName);
        }
        else
        {
            Util.disableObject(objectTagName);
        }
    }
    #endregion // PRIVATE_METHODS
}