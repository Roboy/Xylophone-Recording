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
    private static OnTrackpadPress trackpad_Pressed;
    private delegate void OnTriggerPress();
    private static OnTriggerPress trigger_Pressed;

    private SteamVR_TrackedObject tracked_Object;
    private SteamVR_Controller.Device device;
    private List<Rigidbody> key_Rigidbodies = new List<Rigidbody>();
    private Image roboy_image;
    private bool grip_Button_Pressed = false;
    private bool touchpad_Touched = false;
    private Color white_Color = new Color32(255, 255, 255, 255);
    private Color grey_Color = new Color32(173, 173, 173, 255);
    private bool is_Songboard_Active = false;
    private bool is_Application_ButtomPressed = false;
    private bool restartApp = false;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region PUBLIC_METHODS
    void Start()
    {
        tracked_Object = GetComponent<SteamVR_TrackedObject>();
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

             if (tracked_Object == null)
             {
                 return;
             }
        }
        device = SteamVR_Controller.Input((int)tracked_Object.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            //Show the controller choosing menu
            grip_Button_Pressed = true;
            MakeGameObjectActive(TagsConstants.CONTROLLER_MENU, true);
        }
        if (grip_Button_Pressed && device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //If user do not touche the touchPad just remove the menu
            touchpad_Touched = true;
            grip_Button_Pressed = false;
        }
        if (touchpad_Touched && !device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            touchpad_Touched = false;
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
        if (touchpad_Touched)
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