﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void changeToLevel() {
        SceneManager.LoadScene("Level1");
    }
    public void changeToScores()
    {
        SceneManager.LoadScene("Scores");
    }
    public void changeToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
