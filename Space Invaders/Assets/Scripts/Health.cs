using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int healthPoints;

    public int hp { get { return healthPoints; } set { healthPoints = value; } }

    private ScoreController scoreController;

    private void Awake()
    {
        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>(); ;
    }

    // Function for killing the player
    private void Die()
    {
        if (transform.gameObject.CompareTag("Player"))
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
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

    private void UpdateScore()
    {
        if (transform.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Should increase");
            scoreController.score += GetComponent<EnemyController>().scoreValue;
        }
    }
}
