using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Inpsector variables
    [SerializeField] int healthPoints;

    // Private variables
    private ScoreController scoreController;

    // Properties
    public int hp { get { return healthPoints; } set { healthPoints = value; } }

    // Sets private variables
    private void Awake()
    {
        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>(); ;
    }


    // Function for making the player take damage
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
            UpdateScore();
        }
    }


    // Kills the player by setting it's active to false or destroying it if it's not the player
    private void Die()
    {
        if (transform.gameObject.CompareTag("Player"))
            transform.gameObject.SetActive(false);

        else
            Destroy(this.gameObject);
    }

    // Updates the score when an enemy dies
    private void UpdateScore()
    {
        if (transform.gameObject.CompareTag("Enemy"))
            scoreController.score += GetComponent<EnemyController>().scoreValue;
    }


}
