using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Changing controller model is handled in this class.
/// </summary>
public class Controller : MonoBehaviour
{

    #region PUBLIC_MEMBER_VARIABLES

    public SnapController SnappyFinger;

    #endregion // PUBLIC_MEMBER_VARIABLES

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
    private bool isSongBoardActive = false;
    private bool isApplicationButtomPressed = false;
    private bool restartApp = false;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region PUBLIC_METHODS
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        MakeRoboyStickInvis();
        MakeLightSaberStickInvis();
        MakeChipsStickVisible();
        MakeControllerDisable();
        //MakeGameBoardDisableAtStart();
    }

    void Update()
    {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag(TagsConstants.CONTROLLER_DEVICE);
        if (restartApp || controllers.Length != TagsConstants.NUMBER_OF_CONTROLLER) {
             if (restartApp || controllers.Length != TagsConstants.NUMBER_OF_CONTROLLER) {
                 MakeGameObjectActive(TagsConstants.CONTROLLER_DEVICE, false);
                 MakeGameObjectActive(TagsConstants.ALL_STARWARS_OBJECT, false);
                MakeGameObjectActive(TagsConstants.CINEMA_SCENE, false);
                MakeGameObjectActive(TagsConstants.RESTART_MESSAGE, true);
                 restartApp = true;
                 return;
             }

             if (trackedObject == null)
             {
                 return;
             }
        }
        device = SteamVR_Controller.Input((int)trackedObject.index);

        //CheckAndShowSongBoard();

        //CheckAndHideSongBoard();

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            //Show the controller choosing menu
            gripButtonPressed = true;
            MakeGameObjectActive(TagsConstants.CONTROLLER_MENU, true);
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
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Trigger Pressed!!!");
            if (SnappyFinger != null)
            {
                Debug.Log("Trigger Droppppp!!");
                SnappyFinger.DropItem();
            }

        }
        if (touchPadTouched)
        {

            if (device.GetAxis().x < -0.33f)
            {
                MakeFirstMenuNotGlow();
                MakeSecondMenuNotGlow();
                MakeThirdMenuGlow();
                MakeChipsStickInvis();
                MakeRoboyStickInvis();
                MakeLightSaberStickVisible();
            }
            else if (device.GetAxis().x > -0.33f && device.GetAxis().x < 0.33f)
            {
                MakeSecondMenuGlow();
                MakeFirstMenuNotGlow();
                MakeThirdMenuNotGlow();
                MakeChipsStickInvis();
                MakeLightSaberStickInvis();
                MakeRoboyStickVisible();
            }
            else if (device.GetAxis().x > 0.33f)
            {
                MakeSecondMenuNotGlow();
                MakeFirstMenuGlow();
                MakeThirdMenuNotGlow();
                MakeRoboyStickInvis();
                MakeLightSaberStickInvis();
                MakeChipsStickVisible();
            }
        }

    }
    #endregion // PUBLIC_METHODS
    
    #region PRIVATE_METHODS
    private void MakeControllerDisable()
    {
        MakeGameObjectActive(TagsConstants.RESTART_MESSAGE, false);
        MakeGameObjectActive(TagsConstants.CONTROLLER_MENU, false);
    }

    //private void MakeGameBoardDisableAtStart()
    //{
    //    MakeGameObjectActive(TagsConstants.SONG_BOARD, false);
    //}

    //private void CheckAndShowSongBoard()
    //{
    //    if (!isSongBoardActive && isApplicationButtomPressed && device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
    //    {
    //        //Set parameter for hiding songBoard when user click on menu button next time.
    //        isSongBoardActive = true;
    //        isApplicationButtomPressed = false;
    //    }

    //    if (!isSongBoardActive && device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
    //    {
    //        MakeGameObjectActive(TagsConstants.SONG_BOARD, true);
    //        isApplicationButtomPressed = true;
    //    }
    //}

    //private void CheckAndHideSongBoard()
    //{
    //    if (isSongBoardActive && isApplicationButtomPressed && device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
    //    {
    //        //Set parameter for showing songBoard when user click on menu button next time.
    //        isSongBoardActive = false;
    //        isApplicationButtomPressed = false;
    //    }

    //    if (isSongBoardActive && device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
    //    {
    //        MakeGameObjectActive(TagsConstants.SONG_BOARD, false);
    //        isApplicationButtomPressed = true;
    //    }
    //}

    private void MakeThirdMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag(TagsConstants.SELECTION_MENU_2);
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void MakeSecondMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag(TagsConstants.SELECTION_MENU_1);
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void MakeFirstMenuGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag(TagsConstants.SELECTION_MENU_0);
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(255, 255, 225, 100);
    }

    private void MakeThirdMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag(TagsConstants.SELECTION_MENU_2);
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void MakeSecondMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag(TagsConstants.SELECTION_MENU_1);
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void MakeFirstMenuNotGlow()
    {
        GameObject menu0 = GameObject.FindGameObjectWithTag(TagsConstants.SELECTION_MENU_0);
        Image image = menu0.GetComponent<Image>();
        image.GetComponent<Image>().color = new Color32(174, 174, 174, 255);
    }

    private void MakeLightSaberStickVisible()
    {
        MakeGameObjectActive(TagsConstants.LIGHT_SABER_STICK, true);
        MakeGameObjectActive(TagsConstants.ALL_STARWARS_OBJECT, true);
        MakeGameObjectActive(TagsConstants.CINEMA_SCENE, false);

    }

    private void MakeLightSaberStickInvis()
    {
        MakeGameObjectActive(TagsConstants.LIGHT_SABER_STICK, false);
    }

    private void MakeChipsStickVisible()
    {
        MakeGameObjectActive(TagsConstants.CHIPS_STICK, true);
        MakeGameObjectActive(TagsConstants.ALL_STARWARS_OBJECT, false);
        MakeGameObjectActive(TagsConstants.CINEMA_SCENE, true);
    }

    private void MakeChipsStickInvis()
    {
        MakeGameObjectActive(TagsConstants.CHIPS_STICK, false);
    }

    private void MakeRoboyStickVisible()
    {
        MakeGameObjectActive(TagsConstants.ROBOY_STICK, true);
    }

    private void MakeRoboyStickInvis()
    {
        MakeGameObjectActive(TagsConstants.ROBOY_STICK, false);
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