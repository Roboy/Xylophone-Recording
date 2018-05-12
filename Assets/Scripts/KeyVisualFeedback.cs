using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class KeyVisualFeedback : MonoBehaviour {

    private bool isKeyMoved;
    private Renderer rend;
    private Color originColor;
    private Color highlightColor = new Color(1.0f, 1.0f, 1.0f);
    private const float lerp = 0.2f;

    public float moveTime = 0.1f;
    public Vector3 hitMove = new Vector3(0.0f, -0.1f, 0.0f);

    // Use this for initialization
    void Start () {
        isKeyMoved = false;
        rend = GetComponent<Renderer>();
        originColor = rend.material.GetColor("_Color");
    }
	
	
    public void MoveAndHighlightOnHit()
    {
        StartCoroutine(VisualFeedbackCoroutine());
    }

    private IEnumerator VisualFeedbackCoroutine()
    {
        if(!isKeyMoved)
        {
            transform.Translate(hitMove);
            rend.material.color = Color.Lerp(originColor, highlightColor, lerp);
            isKeyMoved = true;
        }

        yield return new WaitForSeconds(moveTime);

        if(isKeyMoved)
        {
            transform.Translate(-hitMove);
            rend.material.color = originColor;
            isKeyMoved = false;
        }
    }
}
