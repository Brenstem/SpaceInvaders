using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] int lightEnemyRows;
    [SerializeField] int quickEnemyRows;
    [SerializeField] int protectorAmount;
    [SerializeField] int bossEnemyAmount;

    [SerializeField] GameObject lightEnemy;
    [SerializeField] GameObject quickEnemy;
    [SerializeField] GameObject bossEnemy;
    [SerializeField] GameObject protectorEnemy;

    [SerializeField] Transform startingSpawnPos;

    //These two will need range keyword
    [SerializeField] int rows; // not used atm
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
            SpawnBossEnemies();
        }

        spawn = false;
    }

    private void SpawnLightEnemies()
    {
        lightEnemyRows += currentYPos;
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
        quickEnemyRows += currentYPos;
        for (int i = currentYPos; i < quickEnemyRows; i++)
        {
            Instantiate(quickEnemy, new Vector2(startingSpawnPos.position.x - enemyBreathingRoom,
                startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);

            for (int j = 0; j < columns - 1; j++)
            {
                Instantiate(quickEnemy, new Vector2(startingSpawnPos.position.x + j * enemyBreathingRoom,
                    startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);
            }
            currentYPos++;
        }
    }

    private void SpawnBossEnemies()
    {
        int protectorRows = currentYPos;

        if (protectorAmount > 0)
            protectorRows++;

        for (int i = currentYPos; i < protectorRows; i++)
        {
            Instantiate(protectorEnemy, new Vector2(startingSpawnPos.position.x - enemyBreathingRoom,
                startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);

            for (int j = 0; j < bossEnemyAmount * protectorAmount - 1; j++)
            {
                Instantiate(protectorEnemy, new Vector2(startingSpawnPos.position.x + j * enemyBreathingRoom,
                startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);
            }
            currentYPos++;
        }

        int bossRows = currentYPos;

        if (bossEnemyAmount > 0)
            bossRows++;

        for (int i = currentYPos; i < bossRows; i++)
        {
            Instantiate(bossEnemy, new Vector2(startingSpawnPos.position.x - enemyBreathingRoom,
                startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);
            for (int j = 0; j < bossEnemyAmount - 1; j++)
            {

                Instantiate(bossEnemy, new Vector2(startingSpawnPos.position.x + j * enemyBreathingRoom, 
                    startingSpawnPos.position.y + i * enemyBreathingRoom), Quaternion.identity, this.transform);
            }
        }
    }
}
