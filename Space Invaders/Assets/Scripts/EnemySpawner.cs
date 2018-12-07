using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    // Inspector variables 
    [SerializeField] int lightEnemyRows;
    [SerializeField] int quickEnemyRows;
    [SerializeField] int protectorAmount;
    [SerializeField] int bossEnemyAmount;

    [SerializeField] int columns;
    [SerializeField] float enemyBreathingRoom;

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
    private float ySpawnPos;


    // Initialize variables
    private void Awake()
    {
        if (protectorAmount > 0) // Sets the amount of protector rows based on if protectors are used in the scene
            protectorRows++;

        if (bossEnemyAmount > 0) // Sets the amount of boss rows based on if bosses are used in the scene
            bossRows++;

        SetStartPosition(lightEnemy, columns); // If the designer doesn't spawn any light enemies this will break
        ySpawnPos = startingSpawnPos.position.y;
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
        float colliderSizeX = enemyType.GetComponent<BoxCollider2D>().size.x * enemyType.transform.localScale.x + enemyBreathingRoom; // Gets enemy collider width
        float colliderSizeY = enemyType.GetComponent<BoxCollider2D>().size.y * enemyType.transform.localScale.y; // Gets enemy collider height

        rows += currentRow; // Adds the current row count to the amount of rows to be spawned
        SetStartPosition(enemyType, columns); // Sets the start position based on the amount of columns and the collidersize of the enemy type

        for (int i = currentRow; i < rows; i++)
        {
            // Spawns the rest of the enemies on the row
            ySpawnPos += colliderSizeY/2 + lastColliderSizeY/2 + enemyBreathingRoom;
            for (int j = 0; j < columns; j++)
            {
                // Instantiates an enemy at the spawnposition and sets enemyholder as the parent
                Instantiate(enemyType, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                    ySpawnPos), Quaternion.identity, EnemyHolder.transform);
            }
            currentRow++; // Increases which row the game is spawning enemies on
            lastColliderSizeY = colliderSizeY;
        }
    }

    // Sets the spawn position of the enemies
    private void SetStartPosition(GameObject enemyType, int columns)
    {
        float colliderSizeX = enemyType.GetComponent<BoxCollider2D>().size.x * enemyType.transform.localScale.x;
        float formationSizeX = colliderSizeX * columns + enemyBreathingRoom * (columns - 1) - colliderSizeX; // Vector storing the entire size of the enemy formation

        formationSizeX = -formationSizeX / 2; // Centers the x spawn value

        startingSpawnPos.position = new Vector2(formationSizeX, startingSpawnPos.position.y); // Sets the spawnposition to the bottommost left position of the formationsize

    }
}
