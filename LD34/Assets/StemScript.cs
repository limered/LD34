﻿using UnityEngine;
using System.Collections;

public class StemScript : MonoBehaviour {

    public void Active()
    {
        var comp = GetComponent<SpriteRenderer>();
        comp.color = new Color(0, 0, 0);
    }

    public void NoActive()
    {
        var comp = GetComponent<SpriteRenderer>();
        comp.color = new Color(1, 1, 1);
    }
}
