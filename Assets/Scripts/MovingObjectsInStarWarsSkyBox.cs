using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectsInStarWarsSkyBox : MonoBehaviour
{
    
    private float timeCounter;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timeCounter += Time.deltaTime;
        foreach (Transform child in transform)
        {
                float x = Mathf.Cos(timeCounter) / 200;
                float y = Mathf.Sin(timeCounter) / 200;
                float z = 0;
                if (x < 0) z += 0.02F;
                else z -= 0.02F;
                child.position = new Vector3(child.position.x + x, child.position.y + y, child.position.z);
            
        }

        GameObject xWing = GameObject.Find("XWingSelf");
        GameObject[] laserBolts = GameObject.FindGameObjectsWithTag("laserBolt");
        foreach (GameObject laserBolt in laserBolts) {
            laserBolt.transform.Translate(Vector3.up * 10F * Time.deltaTime);
            float laserPos = laserBolt.transform.localPosition.x;
            if (xWing.transform.position.z + 250 < laserPos)
            {
                laserBolt.transform.localPosition = laserBolt.transform.parent.transform.localPosition;
            }
        }
        /*
        GameObject xWing = GameObject.Find("XWingSelf");
        float xWingPos;
        xWingPos = xWing != null ? xWing.transform.localPosition.z : p.transform.localPosition.z;
        if (laserShot != null)
        {
            laserShot.transform.Translate(Vector3.up * 10F * Time.deltaTime);
            float laserPos = laserShot.transform.localPosition.x;

            if (xWingPos + 250 < laserPos)
            {
                laserShot.transform.localPosition = p.transform.localPosition;
            }
        }
        if (p != null) {
            p.transform.Translate(Vector3.right * 4.5F * Time.deltaTime);
            if (p.transform.position.x - xWing.transform.localPosition.x > 50)
            {
                p.transform.position = startingPosition;
            }
        }
        */
    }
}
