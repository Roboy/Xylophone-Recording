
using UnityEngine;

public class MovingObjectsInStarWarsSkyBox : MonoBehaviour
{
    
    private float timeCounter;
    
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
                laserBolt.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        laserBolts = GameObject.FindGameObjectsWithTag("laserBoltXWing");
        foreach (GameObject laserBolt in laserBolts)
        {
            laserBolt.transform.Translate(Vector3.up * 10F * Time.deltaTime);
            float laserPos = laserBolt.transform.localPosition.x;
            if (xWing.transform.position.z + 250 < laserPos)
            {
                laserBolt.transform.localPosition = new Vector3(0, 0, -5);
            }
        }
    }
}
