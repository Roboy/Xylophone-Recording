using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBall : MonoBehaviour {

    [SerializeField] private GameObject hittingBallPrefab;
    private GameObject _hittingBall;

	// Use this for initialization
	void Start () {
        _hittingBall = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            _hittingBall = Instantiate(hittingBallPrefab) as GameObject;
            _hittingBall.transform.position = transform.TransformPoint(Vector3.forward * 0.5f);
            _hittingBall.transform.rotation = transform.rotation;
        }
	}
}
