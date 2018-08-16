using UnityEngine;

public class ThrowedChipsHandler : MonoBehaviour {
    
    public GameObject controller;
    private Vector3 firstPosition;
	// Use this for initialization
	void Start () {
        firstPosition = transform.localPosition;
	}

    // Update is called once per frame
    void Update() {
       
        Vector3 chipsPos = transform.localPosition;

        transform.localPosition = new Vector3(chipsPos.x, chipsPos.y+0.005F,
            chipsPos.z);
        if (Mathf.Abs(firstPosition.y - chipsPos.y) > 0.15) {

            transform.localPosition = firstPosition;
            transform.rotation = controller.transform.rotation;
        }
    }
}
