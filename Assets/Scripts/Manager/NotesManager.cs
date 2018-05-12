using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridge;
using ROSBridgeCustom;

public class NotesManager : Singleton<NotesManager>
{

    private float nextActionTime = 0.0f;
    public float period = 1f;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            SendROSMessage();
        }
    }

    void SendROSMessage()
    {
        MusicalNoteMsg msg;
        string RandomMusicalNote = randomMusicalNote();
        System.DateTime absoluteTime = System.DateTime.Now;
        msg = new MusicalNoteMsg(RandomMusicalNote, absoluteTime);
        ROSBridge.ROSBridge.Instance.Publish(MusicalNotesPublisher.GetMessageTopic(), msg);

        //print("MUSICALNOTE message sent:" + RandomMusicalNote + " " + absoluteTime.ToString());
        print(msg.ToYAMLString());

        /*string linkName = "left_hand";
        List<string> linkNames = new List<string>();
        linkNames.Add(linkName);

        RoboyPoseMsg msg2 = new RoboyPoseMsg("hands",
        linkNames, new Vector3[] { transform.position }, new Quaternion[] { transform.rotation });
        ROSBridge.Instance.Publish(RoboyHandsPublisher.GetMessageTopic(), msg2);
        print(msg2.ToYAMLString());*/
    }

    string randomMusicalNote()
    {
        string MusicalNote;
        List<string> MusicalNotes = new List<string>();
        MusicalNotes.Add("A");
        MusicalNotes.Add("D");
        MusicalNotes.Add("C");

        MusicalNote = MusicalNotes[Random.Range(0, MusicalNotes.Count)];
        return MusicalNote;
    }
}
