using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject gameOverText;

    private Text healthText;

    private void Awake()
    {
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
    {
        CheckDeath();

        if (player.GetComponent<Health>() != null)
            healthText.text = "Health: " + player.GetComponent<Health>().hp.ToString();	
	}

    private void CheckDeath()
    {
        if (player.GetComponent<Health>().hp <= 0)
        {
            //Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }
}
