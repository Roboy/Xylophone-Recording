using UnityEngine;

public class Init : MonoBehaviour {
    
    private GameObject cameraPos;
    void Start()
    {
        
        cameraPos = GameObject.Find("Camera (head) (eye)");
        
        foreach (Transform child in transform)
        {
            Vector3 childPos = child.transform.position;
            child.transform.position = new Vector3(cameraPos.transform.localPosition.x + childPos.x,
            cameraPos.transform.position.y + childPos.y,
            cameraPos.transform.position.z + childPos.z);
        }
       
    }
	
	void Update () {
        
    }
}
