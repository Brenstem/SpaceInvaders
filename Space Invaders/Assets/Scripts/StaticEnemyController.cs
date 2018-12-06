using UnityEngine;

public class StaticEnemyController : MonoBehaviour
{

    // Inspector variables
    [SerializeField] float edgeOffset;
    [SerializeField] float changeStateTimer;

    // Private variables
    private Vector2 viewportSize;
    private float xBounds;
    private bool check = false;
    private float timer = 0;


    // Sets private variables
    private void Awake()
    {
        viewportSize.x = Camera.main.orthographicSize * Camera.main.aspect;
        xBounds = viewportSize.x;

    }

    // Calls move function every tenth of a second
    private void Start()
    {
        InvokeRepeating("MoveStatic", 0, 0.1f);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > changeStateTimer)
        {
            ChangeState();
            timer = 0;
        }
    }

    // Changes direction of all children at once
    private void MoveStatic()
    {
        // Loops through all children
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<EnemyController>().movingState)
            {
                // Checks if their position is bigger/smaller than the maximum size of the camera
                if (this.transform.GetChild(i).transform.position.x > xBounds - this.transform.GetChild(i).GetComponent<BoxCollider2D>().size.x
                    * this.transform.GetChild(i).transform.localScale.x / 2 - edgeOffset ||
                    this.transform.GetChild(i).transform.position.x < -xBounds + this.transform.GetChild(i).GetComponent<BoxCollider2D>().size.x
                    * this.transform.GetChild(i).transform.localScale.x / 2 + edgeOffset)
                { check = true; }
            }
        }

        // Changes direction of all children if one child has hit the bounds
        if (check)
        {
            ChangeDirection();
            check = false;
        }
    }

    // 
    private void ChangeState()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<EnemyController>().ChangeState();
    }

    // Changes direction of all children
    private void ChangeDirection()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<EnemyController>().ChangeDirection();
        }
    }
}
