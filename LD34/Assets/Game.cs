using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {


    public GameObject[] lines; 

    private float currentSpeed = 1.5f;
    private float speedDiff = 0.15f;
    private float lastTick = 0;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        if(Time.time > lastTick + currentSpeed)
        {
            lastTick = Time.time;
            for (int i = 0; i < lines.Length; i++) {
                var comp = lines[i].GetComponent<LineScript>();
                comp.Tick();
            }
        }
    }
}
