using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    // Inspector variables 
    [Range(0, 2)][SerializeField] int lightEnemyRows;
    [Range(0, 2)][SerializeField] int quickEnemyRows;
    [Range(0, 3)][SerializeField] int protectorAmount;
    [Range(0, 3)][SerializeField] int bossEnemyAmount;

    [Range(0, 15)] [SerializeField] int columns;
    [Range(0, 10)] [SerializeField] float enemyBreathingRoom;

    [SerializeField] GameObject lightEnemy;
    [SerializeField] GameObject quickEnemy;
    [SerializeField] GameObject bossEnemy;
    [SerializeField] GameObject protectorEnemy;

    [SerializeField] Transform startingSpawnPos;
    [SerializeField] Transform EnemyHolder;

    // Private variables
    private bool spawn = true;
    private float lastColliderSizeY = 0;
    private int currentRow;
    private int protectorRows;
    private int bossRows;

    private void Awake()
    {
        if (protectorAmount > 0) // Sets the amount of protector rows based on if protectors are used in the scene
            protectorRows++;

        if (bossEnemyAmount > 0) // Sets the amount of boss rows based on if bosses are used in the scene
            bossRows++;
    }

    // Spawns enemies on update if spawn is true
    private void Update()
    {
        if (spawn)
        {
            SpawnEnemies(lightEnemy, lightEnemyRows, columns);
            SpawnEnemies(quickEnemy, quickEnemyRows, columns);
            SpawnEnemies(protectorEnemy, protectorRows, bossEnemyAmount * protectorAmount);
            SpawnEnemies(bossEnemy, bossRows, bossEnemyAmount);
        }

        spawn = false;
    }


    // Spawns enemies based on enemytype, how many rows of enemies and how many columns of enemies
    private void SpawnEnemies(GameObject enemyType, int rows, int columns)
    {
        float colliderSizeX = enemyType.GetComponent<BoxCollider2D>().size.x
            * enemyType.GetComponent<Transform>().localScale.x + enemyBreathingRoom; // Gets enemy collider width

        float colliderSizeY = enemyType.GetComponent<BoxCollider2D>().size.y *
            enemyType.GetComponent<Transform>().localScale.y; // Gets enemy collider height

        rows += currentRow; // Adds the current row count to the amount of rows to be spawned
        SetStartPosition(enemyType, columns); // Sets the start position based on the amount of columns and the collidersize of the enemy type

        // Spawns the first enemy of each row based on the current row count and the amount of rows to be spawned
        for (int i = currentRow; i < rows; i++)
        {
            // Instantiates an enemy at the spawnposition and sets enemyholder as the parent
            Instantiate(enemyType, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);

            // Spawns the rest of the enemies on the row
            for (int j = 1; j < columns; j++)
            {
                // Instantiates an enemy at the spawnposition and sets enemyholder as the parent

                Instantiate(enemyType, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                    startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);
            }
            currentRow++; // Increases which row the game is spawning enemies on
            lastColliderSizeY = colliderSizeY;
        }
    }

    // Sets the spawn position of the enemies
    private void SetStartPosition(GameObject enemyType, int columns)
    {
        float colliderSizeX = enemyType.GetComponent<BoxCollider2D>().size.x * enemyType.GetComponent<Transform>().localScale.x;

        //Debug.Log("collider size " + colliderSizeY / 2);
        //Debug.Log("last collider size setstart" + lastColliderSizeY);
        //Debug.Log("current row " + currentRow);

        // Vector storing the entire size of the enemy formation
        Vector2 formationSize = new Vector2(colliderSizeX * columns + enemyBreathingRoom * (columns - 1) - colliderSizeX, 0);

        // Centers the x spawn value
        formationSize.x = -formationSize.x / 2;

        // Sets the spawnposition to the bottommost left position of the formationsize
        startingSpawnPos.position = formationSize;
    }
}
