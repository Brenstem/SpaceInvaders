using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private int mScore;

    private Text scoreText;

    public int score {
        get { return mScore; }
        set { mScore = value; }
    }

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        scoreText.text = "Score: " + mScore.ToString();
	}
}
