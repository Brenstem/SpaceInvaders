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

    //These two will need range keyword
    //[SerializeField] int rows; // not used atm
    [SerializeField] int columns;
    [SerializeField] float enemyBreathingRoom;

    Vector2 viewportSize;
    float xBounds;

    //debugging
    bool spawn = true;
    int currentYPos;

    private void Start()
    {
        viewportSize = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        xBounds = viewportSize.x;
        InvokeRepeating("moveStatic", 0, 0.1f);
    }

    // Update is called once per frame
    private void Update()
    {
        enemyBreathingRoom += 0.1f;
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

        Debug.Log(colliderSizeX - 0.5f);
        lightEnemyRows += currentYPos;
        SetStartPosition(lightEnemy);

        for (int i = currentYPos; i < lightEnemyRows; i++)
        {
            Debug.Log(startingSpawnPos.position.x);
            Instantiate(lightEnemy, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * colliderSizeX), Quaternion.identity, this.transform);
            Debug.Log(startingSpawnPos.position.x);


            for (int j = 1; j < columns; j++)
            {
                Instantiate(lightEnemy, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                    startingSpawnPos.position.y + i * colliderSizeX), Quaternion.identity, this.transform);
            }
            currentYPos++;
        }
    }

    private void SpawnQuickEnemies()
    {
        float colliderSizeX = quickEnemy.GetComponent<BoxCollider2D>().size.x * quickEnemy.GetComponent<Transform>().localScale.x + enemyBreathingRoom;
        quickEnemyRows += currentYPos;
        SetStartPosition(quickEnemy);

        for (int i = currentYPos; i < quickEnemyRows; i++)
        {
            Instantiate(quickEnemy, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * colliderSizeX), Quaternion.identity, this.transform);

            for (int j = 1; j < columns; j++)
            {
                Instantiate(quickEnemy, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                    startingSpawnPos.position.y + i * colliderSizeX), Quaternion.identity, this.transform);
            }
            currentYPos++;
        }
    }

    private void spawnProtectors()
    {
        float colliderSizeX = protectorEnemy.GetComponent<BoxCollider2D>().size.x * protectorEnemy.GetComponent<Transform>().localScale.x + enemyBreathingRoom;
        int protectorRows = currentYPos;
        SetStartPosition(protectorEnemy);

        if (protectorAmount > 0)
            protectorRows++;

        for (int i = currentYPos; i < protectorRows; i++)
        {
            Instantiate(protectorEnemy, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * colliderSizeX), Quaternion.identity, this.transform);

            for (int j = 1; j < bossEnemyAmount * protectorAmount; j++)
            {
                Instantiate(protectorEnemy, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                startingSpawnPos.position.y + i * colliderSizeX), Quaternion.identity, this.transform);
            }
            currentYPos++;
        }
    }

    private void SpawnBossEnemies()
    {
        float colliderSizeX = bossEnemy.GetComponent<BoxCollider2D>().size.x * bossEnemy.GetComponent<Transform>().localScale.x + enemyBreathingRoom;
        int bossRows = currentYPos;
        SetStartPosition(bossEnemy);

        if (bossEnemyAmount > 0)
            bossRows++;

        for (int i = currentYPos; i < bossRows; i++)
        {
            Instantiate(bossEnemy, new Vector2(startingSpawnPos.position.x,
                startingSpawnPos.position.y + i * colliderSizeX), Quaternion.identity, this.transform);

            for (int j = 1; j < bossEnemyAmount; j++)
            {

                Instantiate(bossEnemy, new Vector2(startingSpawnPos.position.x + j * colliderSizeX,
                    startingSpawnPos.position.y + i * colliderSizeX), Quaternion.identity, this.transform);
            }
        }
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
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<EnemyController>().ChangeDirection();
        }
    }

    private void SetStartPosition(GameObject enemyType)
    {
        float colliderSizeX = enemyType.GetComponent<BoxCollider2D>().size.x * enemyType.GetComponent<Transform>().localScale.x;
        Vector2 formationSize = new Vector2(columns * colliderSizeX, 0);

        startingSpawnPos.position = -formationSize/2;
    }
}
