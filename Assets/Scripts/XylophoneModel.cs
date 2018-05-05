using UnityEngine;

public class XylophoneModel : MonoBehaviour {

    //Notes color which is green
    public Color noteColor = Color.green;
    //xylophone color which is red
    public Color xylophoneColor = Color.red;
    // Use this for initialization
    void Start()
    {
        //numberOfNotes is the number of notes which can be assigned by users
        int numberOfNotes;
        //notesMinSize which used for size of note in y boarder and z boarder
        //notesMaxSize which used for size of note in x boarder
        //notesPositionScale used for distance between notes
        //notesStartPosition position of first notes
        float notesMinSize, notesMaxSize, notesSizeScale, notesPositionScale, notesStartPosition;
        
        ScaleXylophoneNotesSize(out numberOfNotes, out notesMinSize, out notesMaxSize, out notesSizeScale,
            out notesPositionScale, out notesStartPosition);
        CreatexylophoneBound(numberOfNotes, notesMinSize, notesMaxSize, notesSizeScale, 
            notesStartPosition);
        CreateXylophoneNotes(numberOfNotes, notesMinSize, notesMaxSize, notesSizeScale, 
            notesPositionScale, notesStartPosition);

    }

    /// <summary>
    /// This methode will create the xylophone red frame based on the note size and notes distance.
    /// </summary>
    /// <param name="numberOfNotes">number of notes</param>
    /// <param name="notesMinSize">size of notes on y and z boarder</param>
    /// <param name="notesMaxSize">size of notes on x boarder</param>
    /// <param name="notesSizeScale">size of xylophone based on number o notes and notesSize</param>
    /// <param name="notesStartPosition">position of first note</param>
    private static void CreatexylophoneBound(int numberOfNotes, float notesMinSize, float notesMaxSize, 
        float notesSizeScale, float notesStartPosition)
    {
        float xtlophoneStartPosition = notesStartPosition;
        float boundAngle = (float)(180.0 / Mathf.PI) * Mathf.Atan((notesMaxSize / 2) / (-notesStartPosition * 2));
        float xylophoneLength = 1.25F*(notesMaxSize/(2*Mathf.Sin(Mathf.Atan((notesMaxSize / 2) / (-notesStartPosition * 2)))));

        GameObject lowerBound = GameObject.CreatePrimitive(PrimitiveType.Cube);
        lowerBound.transform.localScale = new Vector3(notesMinSize, notesMinSize, xylophoneLength);
        lowerBound.transform.position = new Vector3(numberOfNotes / 2 * notesSizeScale, 0, 0);
        lowerBound.transform.Rotate(0, -boundAngle, 0);
        Renderer lowerRender = lowerBound.GetComponent<Renderer>();
        lowerRender.material.shader = Shader.Find("Specular");
        lowerRender.material.SetColor("_Color", Color.red);


        GameObject upperBound = GameObject.CreatePrimitive(PrimitiveType.Cube);
        upperBound.transform.localScale = new Vector3(notesMinSize, notesMinSize, xylophoneLength);
        upperBound.transform.position = new Vector3(-numberOfNotes / 2 * notesSizeScale, 0, 0);
        upperBound.transform.Rotate(0, boundAngle, 0);
        Renderer upperRender = upperBound.GetComponent<Renderer>();
        upperRender.material.shader = Shader.Find("Specular");
        upperRender.material.SetColor("_Color", Color.red);
    }
    /// <summary>
    /// This methode will find the position of first note based on the note size on y boarder(notesMinSize) and distance between 
    /// notes(sizeScale) and rescale it on (0,0,0)
    /// </summary>
    /// <param name="numberOfNotes">number of notes</param>
    /// <param name="notesMinSize">size of notes on y and z boarder</param>
    /// <param name="notesMaxSize">size of notes on x boarder</param>
    /// <param name="sizeScale">size of xylophone based on number o notes and notesSize</param>
    /// <param name="notesDistance">distance between notes</param>
    /// <param name="firstNotePosition">position of first note</param>
    private static void ScaleXylophoneNotesSize(out int numberOfNotes, out float notesMinSize, out float notesMaxSize, 
        out float sizeScale, out float notesDistance, out float firstNotePosition)
    {
        numberOfNotes = 5;
        notesMinSize = 0.5F;
        notesMaxSize = 3;
        sizeScale = 0;
        sizeScale = (notesMaxSize - notesMinSize) / numberOfNotes;

        notesDistance = 1F;
        if (numberOfNotes % 2 == 0)
        {
            firstNotePosition = -(((numberOfNotes / 2)-1) * notesDistance + notesDistance/2);
        }
        else
        {
            firstNotePosition = -(((int)(numberOfNotes / 2)) * notesDistance);
        }
    }
    /// <summary>
    /// based on the sizes which found on ScaleXylophoneNotesSize, it creates the notes and small white circles above them
    /// </summary>
    /// <param name="numberOfNotes">number of notes</param>
    /// <param name="notesMinSize">size of notes on y and z boarder</param>
    /// <param name="noteMaxSize">size of notes on x boarder</param>
    /// <param name="sizeScale">size of xylophone based on number o notes and notesSize</param>
    /// <param name="notesDistance">distance between notes</param>
    /// <param name="firstNotePosition">position of first note</param>
    private void CreateXylophoneNotes(int numberOfNotes, float notesMinSize, float noteMaxSize, 
        float sizeScale, float notesDistance, float firstNotePosition)
    {
        for (int i = 0; i < numberOfNotes; i++)
        {
            GameObject note = GameObject.CreatePrimitive(PrimitiveType.Cube);
            note.transform.localScale = new Vector3(1.25F*(noteMaxSize - sizeScale * i), notesMinSize, notesMinSize);
            note.transform.position = new Vector3(0, notesMinSize, firstNotePosition + notesDistance * i);
            note.GetComponent<Renderer>().material.color = new Color(noteColor.r, noteColor.g, noteColor.b, noteColor.a);
            //create small white button on the right hand side of note
            GameObject rightButton = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            rightButton.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
            rightButton.transform.position = new Vector3((noteMaxSize - sizeScale * i)/2, 1.5F*notesMinSize, firstNotePosition + notesDistance * i);
            //create small white button on the left hand side of note
            GameObject leftButton = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            leftButton.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
            leftButton.transform.position = new Vector3(-(noteMaxSize - sizeScale * i) / 2, 1.5F*notesMinSize, firstNotePosition + notesDistance * i);
        }
    }

    // Update is called once per frame, before rendering update
    void Update () {
		
	}
    
}
