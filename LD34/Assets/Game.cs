using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {


    public GameObject[] lines;
    public GameObject[] stemsR;
    public GameObject[] stemsL;
    public GameObject[] flowersR;
    public GameObject[] flowersL;

    public GameObject pointsText;
    public GameObject liveText;

    public Canvas startScreen;
    public Canvas endScreen;
    public Text endPoints;

    public AudioSource beep;
    public AudioSource pointBeep;
    public AudioSource lostBeep;

    private float currentSpeed = 1.5f;
    private float speedDiff = 0.15f;
    private float nextSpeedUp = 50;
    private float lastTick = 0;

    private int flowerPosition = 2;

    private bool running = false;

    private float points = 0;
    private int lives = 3;

    // Use this for initialization
    void Start () {
        ActivateStemsForPosition();
        startScreen.enabled = true;
        endScreen.enabled = false;
    }
    
    // Update is called once per frame
    void Update () {
        if (!running)
        {
            CheckStart();
        }
        else
        {
            CheckMove();
            if (Time.time > lastTick + currentSpeed)
            {
                lastTick = Time.time;

                for (int i = 0; i < lines.Length; i++)
                {
                    var comp = lines[i].GetComponent<LineScript>();
                    var livelost = comp.Tick();
                    if (livelost)
                    {
                        lives--;
                        liveText.GetComponent<Text>().text = (lives >= 0) ? lives.ToString() : "0";
                    }

                }
            }
            if (lives <= 0)
            {
                EndGame();
            }
            CheckPoints();
            CheckSpeedUp();

            pointsText.GetComponent<Text>().text = Mathf.Floor(points).ToString();
        }
    }

    private void EndGame()
    {
        endPoints.text = "Points: " + Mathf.Floor(points).ToString();
        endScreen.enabled = true;
        running = false;
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
            nextSpeedUp = points + points * 2f;
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

    private void CheckStart()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            startScreen.enabled = false;
            endScreen.enabled = false;
            lives = 3;
            liveText.GetComponent<Text>().text = (lives >= 0) ? lives.ToString() : "0";
            points = 0;
            currentSpeed = 1.5f;
            nextSpeedUp = 50;
            lastTick = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                var comp = lines[i].GetComponent<LineScript>();
                comp.Collect();
            }
            running = true;
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
