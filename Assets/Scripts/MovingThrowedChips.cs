
using UnityEngine;

public class MovingThrowedChips : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = new Vector3(this.transform.position.x,
            this.transform.position.y, 
            this.transform.position.z+1);
        if (this.transform.position.z > 10) {
            Destroy(this);
        }
    }
}
