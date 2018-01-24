using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScores : MonoBehaviour {

    public Text scoreText;
    public Text timeText;
    // Use this for initialization
    void Start () {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("scoreCount");
        timeText.text = "Time: " + PlayerPrefs.GetFloat("time");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
