using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject lightEnemy;
    [SerializeField] GameObject quickEnemy;
    [SerializeField] GameObject bossEnemy;
    [SerializeField] GameObject protectorEnemy;

    [SerializeField] int lightEnemyAmount;
    [SerializeField] int quickEnemyAmount;
    [SerializeField] int bossEnemyAmount;
    [SerializeField] int protectorPerBossAmount;

    [SerializeField] Transform startingSpawnPos;

    //These two will need range keyword
    [SerializeField] int rows;
    [SerializeField] int columns;

    [SerializeField] float enemyBreathingRoom;

    private GameObject[] enemies;

	// Use this for initialization
	void Start ()
    {
        SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void SpawnEnemies()
    {
        Transform spawnPosition = startingSpawnPos;
        BoxCollider2D enemyCollider = lightEnemy.GetComponent<BoxCollider2D>();
        
        for (int i = 0; i < columns; i++)
        {
            Instantiate(lightEnemy, spawnPosition);
            
        }
    }
}
