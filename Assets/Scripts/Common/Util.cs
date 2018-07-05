using System.Collections.Generic;
using UnityEngine;

public class Util{

    private static Dictionary<string, GameObject> objectsRefMap = new Dictionary<string, GameObject>();

    public static void enableObject(string objectTagName)
    {
        int counter = 0;
        while (true)
        {
            if (objectsRefMap.ContainsKey(objectTagName + counter))
            {
                objectsRefMap[objectTagName + counter].SetActive(true);
                counter++;
            }
            else {
                break;
            }
        }
    }

    public static void disableObject(string objectTagName)
    {

        GameObject[] objectRefs = GameObject.FindGameObjectsWithTag(objectTagName);
        int counter = 0;
        foreach (GameObject objectRef in objectRefs)
        {
            objectsRefMap[objectTagName+counter] = objectRef;
            objectsRefMap[objectTagName+counter].SetActive(false);
            counter++;
        }
        
    }


}
