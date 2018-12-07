using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    // Private variables
    private int mScore;
    private Text scoreText;

    // Properties
    public int score { get { return mScore; } set { mScore = value; } }

    // Sets private variables
    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

	// Updates score text
	private void Update ()
    {
        scoreText.text = "Score: " + mScore.ToString();
	}
}
