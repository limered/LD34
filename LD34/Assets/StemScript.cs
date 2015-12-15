using UnityEngine;
using System.Collections;

public class StemScript : MonoBehaviour {

    public Color activeColor;

    public void Active()
    {
        var comp = GetComponent<SpriteRenderer>();
        comp.color = activeColor;
    }

    public void NoActive()
    {
        var comp = GetComponent<SpriteRenderer>();
        comp.color = new Color(1, 1, 1, 0.5f);
    }
}
