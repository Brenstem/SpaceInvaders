using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    [Range(0, 2)]
    [SerializeField] int lightEnemyRows;
    [Range(0, 2)]
    [SerializeField] int quickEnemyRows;
    [Range(0, 3)]
    [SerializeField] int protectorAmount;
    [Range(0, 3)]
    [SerializeField] int bossEnemyAmount;

    [SerializeField] GameObject lightEnemy;
    [SerializeField] GameObject quickEnemy;
    [SerializeField] GameObject bossEnemy;
    [SerializeField] GameObject protectorEnemy;

    [SerializeField] Camera camera;
    [SerializeField] Transform startingSpawnPos;
    [SerializeField] Transform EnemyHolder;

    //These two will need range keyword
    //[SerializeField] int rows; // not used atm
    [SerializeField] int columns;
    [SerializeField] float enemyBreathingRoom;


    Vector2 viewportSize;
    float xBounds;

    //debugging
    bool spawn = true;
    float lastColliderSizeY = 0;
    int currentRow;

    private void Start()
    {
        viewportSize = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        xBounds = viewportSize.x;
        InvokeRepeating("moveStatic", 0, 0.1f);
    }

    private void Update()
    {
        if (spawn)
        {
            SpawnLightEnemies();
            SpawnQuickEnemies();
            spawnProtectors();
            SpawnBossEnemies();
        }

        spawn = false;
    }

    private void SpawnLightEnemies()
    {
        float colliderSizeX = lightEnemy.GetComponent<BoxCollider2D>().size.x
            * lightEnemy.GetComponent<Transform>().localScale.x + enemyBreathingRoom;

        float colliderSizeY = lightEnemy.GetComponent<BoxCollider2D>().size.y *
            lightEnemy.GetComponent<Transform>().localScale.y;

        lightEnemyRows += currentRow;
        SetStartPosition(lightEnemy, columns);

        for (int i = currentRow; i < lightEnemyRows; i++)
        {
            Instantiate(lightEnemy, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);

            for (int j = 1; j < columns; j++)
            {
                Instantiate(lightEnemy, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                    startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);
            }
            currentRow++;
            lastColliderSizeY = colliderSizeY;
        }
    }

    private void SpawnQuickEnemies()
    {
        float colliderSizeX = quickEnemy.GetComponent<BoxCollider2D>().size.x *
            quickEnemy.GetComponent<Transform>().localScale.x + +enemyBreathingRoom;

        float colliderSizeY = quickEnemy.GetComponent<BoxCollider2D>().size.y *
            quickEnemy.GetComponent<Transform>().localScale.y;

        quickEnemyRows += currentRow;
        SetStartPosition(quickEnemy, columns);

        for (int i = currentRow; i < quickEnemyRows; i++)
        {
            Instantiate(quickEnemy, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);

            for (int j = 1; j < columns; j++)
            {
                Instantiate(quickEnemy, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                    startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);
            }

            Debug.Log("last collider size" + lastColliderSizeY);
            Debug.Log("collider size" + colliderSizeY);
            Debug.Log("Breathing room " + enemyBreathingRoom);
            Debug.Log("currentRow " + i);

            Debug.Log(i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2);

            currentRow++;
            lastColliderSizeY = colliderSizeY;
        }
    }

    private void spawnProtectors()
    {
        float colliderSizeX = protectorEnemy.GetComponent<BoxCollider2D>().size.x *
            protectorEnemy.GetComponent<Transform>().localScale.x + enemyBreathingRoom;

        float colliderSizeY = protectorEnemy.GetComponent<BoxCollider2D>().size.y *
    protectorEnemy.GetComponent<Transform>().localScale.y;

        int protectorRows = currentRow;
        SetStartPosition(protectorEnemy, protectorAmount * bossEnemyAmount);

        if (protectorAmount > 0)
            protectorRows++;

        for (int i = currentRow; i < protectorRows; i++)
        {
            Instantiate(protectorEnemy, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);

            for (int j = 1; j < bossEnemyAmount * protectorAmount; j++)
            {
                Instantiate(protectorEnemy, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);
            }
            currentRow++;
            lastColliderSizeY = colliderSizeY;
        }
    }

    private void SpawnBossEnemies()
    {
        float colliderSizeX = bossEnemy.GetComponent<BoxCollider2D>().size.x
            * bossEnemy.GetComponent<Transform>().localScale.x + enemyBreathingRoom;

        float colliderSizeY = bossEnemy.GetComponent<BoxCollider2D>().size.y 
            * bossEnemy.GetComponent<Transform>().localScale.y; 

        int bossRows = currentRow;

        SetStartPosition(bossEnemy, bossEnemyAmount);

        if (bossEnemyAmount > 0)
            bossRows++;

        for (int i = currentRow; i < bossRows; i++)
        {
            Instantiate(bossEnemy, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);

            for (int j = 1; j < bossEnemyAmount; j++)
            {

                Instantiate(bossEnemy, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                    startingSpawnPos.position.y + i * (colliderSizeY + lastColliderSizeY + enemyBreathingRoom) / 2), Quaternion.identity, EnemyHolder.transform);
            }
            currentRow++;
        }
        lastColliderSizeY = colliderSizeY;
    }

    private void moveStatic()
    {
        bool check = false;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<Transform>().position.x > xBounds
                - this.transform.GetChild(i).GetComponent<BoxCollider2D>().size.x / 2 ||
                this.transform.GetChild(i).GetComponent<Transform>().position.x <
                -xBounds + this.transform.GetChild(i).GetComponent<BoxCollider2D>().size.x / 2)
            {
                check = true;
            }
        }

        if (check)
        {
            ChangeDirection();
            check = false;
        }
    }

    private void ChangeDirection()
    {
        for (int i = 0; i < EnemyHolder.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<EnemyController>().ChangeDirection();
        }
    }

    private void SetStartPosition(GameObject enemyType, int columns)
    {
        float colliderSizeX = enemyType.GetComponent<BoxCollider2D>().size.x * enemyType.GetComponent<Transform>().localScale.x;

        //Debug.Log("collider size " + colliderSizeY / 2);
        //Debug.Log("last collider size setstart" + lastColliderSizeY);
        //Debug.Log("current row " + currentRow);

        Vector2 formationSize = new Vector2(colliderSizeX * columns + enemyBreathingRoom * (columns - 1) - colliderSizeX, 0);

        formationSize.x = -formationSize.x / 2;

        startingSpawnPos.position = formationSize;
    }
}
