using UnityEngine;

public class Init : MonoBehaviour {

    // Use this for initialization
    private GameObject xWing;
    private GameObject cameraPos;
    private GameObject tieFighters;
    private GameObject xylophone;
    private Vector3 xWingPos;
    private Vector3 tieFightersPos;
    private Vector3 xylophonePos;
	void Start () {

        xWing = GameObject.Find("XWings");
        tieFighters = GameObject.Find("Tie fighters");
        xylophone = GameObject.Find("Xylophone");

        xWingPos = xWing.transform.localPosition;
        tieFightersPos = tieFighters.transform.localPosition;
        xylophonePos = xylophone.transform.localPosition;

        cameraPos = GameObject.Find("Camera (head) (eye)");
        xWing.transform.localPosition = new Vector3(cameraPos.transform.localPosition.x + xWingPos.x,
            cameraPos.transform.localPosition.y + xWingPos.y,
            cameraPos.transform.localPosition.z + xWingPos.z);
        tieFighters.transform.localPosition = new Vector3(cameraPos.transform.localPosition.x + tieFightersPos.x,
            cameraPos.transform.localPosition.y + tieFightersPos.y,
            cameraPos.transform.localPosition.z + tieFightersPos.z);
        xylophone.transform.localPosition = new Vector3(cameraPos.transform.localPosition.x + xylophonePos.x,
            cameraPos.transform.localPosition.y + xylophonePos.y,
            cameraPos.transform.localPosition.z + xylophonePos.z);

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
