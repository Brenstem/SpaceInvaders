using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Inspector variables 
    [SerializeField] float staticSpeed;
    [SerializeField] float movingSpeed;
    [SerializeField] int hp;
    [Tooltip("Multiplier for x axis movement during moving state")] [SerializeField] float XSpeedMultiplier;

    // Private variables
    private Rigidbody2D rb;
    private Vector3 movement;
    private GameObject target;
    private Vector2 vectorToTarget;

    public bool movingState = false;

    private bool initialMove = true;


    // Gets components and sets private variables
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(1, 0) * staticSpeed * Time.fixedDeltaTime;
    }

    // Finds player
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        vectorToTarget = new Vector2(target.transform.position.x - this.transform.position.x, -1);
        vectorToTarget = vectorToTarget / vectorToTarget.magnitude;
        vectorToTarget = vectorToTarget * movingSpeed * Time.fixedDeltaTime;
        vectorToTarget.x *= XSpeedMultiplier;
    }

    // Physics based movement
    private void FixedUpdate()
    {
        Move();
    }

    // Moves the player based on the movement vector
    private void Move()
    {
        if (!movingState)
            rb.MovePosition(transform.position + movement);

        else if (movingState)
        {
            if (initialMove)
            {
                rb.AddForce(Vector2.up * 3000 * Time.fixedDeltaTime);
                rb.AddForce(Vector2.right * 3000 * Time.fixedDeltaTime);

                initialMove = false;
            }

            rb.AddForce(vectorToTarget );
        }
    }

    // Function for killing the player
    private void Die()
    {
        Destroy(this.gameObject);
    }

    // Function for making the player take damage
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
    }

    // Function for making the player change direction
    public void ChangeDirection()
    {
        movement = -movement;
    }

    // Changes the state of enemy to moving
    public void ChangeState()
    {
        movingState = true;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Loop")
        {
            transform.position = new Vector2(transform.position.x, 7);
        }
    }

}
