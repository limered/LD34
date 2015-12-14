using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {


    public GameObject[] lines;
    public GameObject[] stemsR;
    public GameObject[] stemsL;
    public GameObject[] flowersR;
    public GameObject[] flowersL;

    private float currentSpeed = 1.5f;
    private float speedDiff = 0.15f;
    private float nextSpeedUp = 50;
    private float lastTick = 0;

    private int flowerPosition = 2;

    public float points = 0;
    public int lives = 3;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        CheckMove();
        if (Time.time > lastTick + currentSpeed)
        {
            lastTick = Time.time;
            for (int i = 0; i < lines.Length; i++) {
                var comp = lines[i].GetComponent<LineScript>();
                comp.Tick();
            }
        }
        CheckPoints();
        CheckSpeedUp();
        CheckLives();
    }

    private void CheckLives() {
        for (int i = 0; i < lines.Length; i++)
        {
            var comp = lines[i].GetComponent<LineScript>();
            if(comp.currentDrop == 5)
            {
                lives--;
            }
        }
        if(lives == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {

    }

    private void CheckPoints() {
        var currentLine = lines[flowerPosition].GetComponent<LineScript>();
        if (currentLine.currentDrop == 4)
        {
            points += 0.5f * Time.time;
            currentLine.Collect();
        }
    }

    private void CheckSpeedUp() {
        if(points != 0 && points > nextSpeedUp)
        {
            currentSpeed -= speedDiff;
            nextSpeedUp = points + points * 2;
        }
    }

    private void CheckMove() {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangePosition(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangePosition(true);
        }
    }

    private void ChangePosition(bool left) {
        if (left)
            flowerPosition = (flowerPosition == 0) ? 0 : flowerPosition-1;
        else
            flowerPosition = (flowerPosition == 5) ? 5 : flowerPosition+1;
        ActivateStemsForPosition();
    }

    private void ActivateStemsForPosition()
    {
        for(int i = 0; i < 3; i++)
        {
            stemsL[i].GetComponent<StemScript>().NoActive();
            stemsR[i].GetComponent<StemScript>().NoActive();
            flowersL[i].GetComponent<StemScript>().NoActive();
            flowersR[i].GetComponent<StemScript>().NoActive();
        }
        if(flowerPosition <= 2)
        {
            for(int i = 2; i >= flowerPosition; i--)
            {
                stemsL[i].GetComponent<StemScript>().Active();
            }
            flowersL[flowerPosition].GetComponent<StemScript>().Active();
        }
        else
        {
            for (int i = 3; i <= flowerPosition; i++)
            {
                stemsR[i-3].GetComponent<StemScript>().Active();
            }
            flowersR[flowerPosition-3].GetComponent<StemScript>().Active();
        }
    }
}
