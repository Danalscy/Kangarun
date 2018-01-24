using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    // Use this for initialization
    public Text scoreText;
    public Text timeText;
    public int scoreCount;
    public float time;
    public  int highScore;
    void Start () {
        time = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "Score: " + scoreCount;

            //code something
        time = Mathf.FloorToInt(Time.time % 60f);
        timeText.text = "Time: " + time;
    }
    public void AddScore()
    {
        scoreCount += 1;
    }

}
