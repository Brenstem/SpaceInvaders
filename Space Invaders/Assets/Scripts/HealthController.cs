using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    // Inspector variables
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameOverText;

    // Private variables
    private Text healthText;


    // Sets private variables
    private void Awake()
    {
        healthText = GetComponent<Text>();
    }

    // Checks for player death and updates the UI health elemetnt
    private void Update ()
    {
        CheckDeath();

        if (player.GetComponent<Health>() != null)
            healthText.text = "Health: " + player.GetComponent<Health>().hp.ToString();	
	}


    // Freezes game and shows gameover UI element if player dies
    private void CheckDeath()
    {
        if (player.GetComponent<Health>().hp <= 0)
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }
}
