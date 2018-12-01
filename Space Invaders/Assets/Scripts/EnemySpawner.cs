using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] int lightEnemyRows;
    [SerializeField] int quickEnemyRows;

    [SerializeField] GameObject lightEnemy;
    [SerializeField] GameObject quickEnemy;
    [SerializeField] GameObject bossEnemy;
    [SerializeField] GameObject protectorEnemy;

    [SerializeField] Transform startingSpawnPos;

    //These two will need range keyword
    [SerializeField] int rows;
    [SerializeField] int columns;

    [SerializeField] float enemyBreathingRoom;

    //debugging
    bool spawn = true;
    int currentYPos;


    // Update is called once per frame
    void Update ()
    {
        if (spawn)
        {
            SpawnLightEnemies();
            SpawnQuickEnemies();
        }

        spawn = false;
    }

    private void SpawnLightEnemies()
    {
        for (int i = currentYPos; i < lightEnemyRows; i++)
        {
            Instantiate(lightEnemy, new Vector2(startingSpawnPos.position.x - enemyBreathingRoom,
                startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);

            for (int j = 0; j < columns-1; j++)
            {
                Instantiate(lightEnemy, new Vector2(startingSpawnPos.position.x + j * enemyBreathingRoom,
                    startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);
            }
            currentYPos++;
        }
    }
    private void SpawnQuickEnemies()
    {
        for (int i = currentYPos; i < quickEnemyRows + currentYPos; i++)
        {
            Instantiate(quickEnemy, new Vector2(startingSpawnPos.position.x - enemyBreathingRoom,
                startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);

            for (int j = 0; j < columns - 1; j++)
            {
                Instantiate(quickEnemy, new Vector2(startingSpawnPos.position.x + j * enemyBreathingRoom,
                    startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);
            }
        }
    }
}
