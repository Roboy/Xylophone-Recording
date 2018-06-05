using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectsInStarWarsSkyBox : MonoBehaviour
{

    // Use this for initialization
    public GameObject laserShot;
    public GameObject parent;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject xWing = GameObject.Find("XWingSelf");
        float xWingPos;
        xWingPos = xWing != null ? xWing.transform.localPosition.z : parent.transform.localPosition.z;
        if (laserShot != null)
        {
            laserShot.transform.Translate(Vector3.up * 10F * Time.deltaTime);
            float laserPos = laserShot.transform.localPosition.x;

            if (xWingPos + 250 < laserPos)
            {
                laserShot.transform.localPosition = parent.transform.localPosition;
            }
        }
        if (parent != null) {
            //parent.transform.Translate(Vector3.right * 0.5F * Time.deltaTime);
        }

    }
}
