using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CircularMenu : MonoBehaviour {

    private List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 mousePosition;
    private Vector2 toVector2Me;
    private Vector2 fromVector2Me = new Vector2(0.5f, 0.1f);
    private Vector2 centerCircle = new Vector2(0.5f, 0.1f);

    private int menuItem;
    private int curMenuItem;
    private int oledMenuItem;
    // Use this for initialization
    void Start () {
        menuItem = buttons.Count;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void getCurrentMenuItem() {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        toVector2Me = new Vector2(mousePosition.x/Screen.width, mousePosition.y / Screen.height);
        float angle = (Mathf.Atan2(fromVector2Me.y-centerCircle.y, fromVector2Me.x-centerCircle.x) - Mathf.Atan2(fromVector2Me.y-centerCircle.y, fromVector2Me.x-centerCircle.x))* Mathf.Deg2Rad;
        
    }
}
[System.Serializable]
public class MenuButton {

    public string name;
    public Image sceneImage;
    public Color normalColor = Color.blue;
    public Color highlightedColor = Color.green;
    public Color pressedColor = Color.red;

}