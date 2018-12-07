using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Inspector variables 
    [SerializeField] float staticSpeed;
    [SerializeField] float movingSpeed;
    [SerializeField] int mScoreValue;
    [Tooltip("Multiplier for x axis movement during moving state")] [SerializeField] float XSpeedMultiplier;

    // Private variables
    private Rigidbody2D rb;
    private Vector3 movement;
    private GameObject target;
    private Vector2 vectorToTarget;
    private Weapon[] weapons;
    private bool initialMove = true;
    private bool mMovingState = false;

    // Properties
    public int scoreValue { get { return mScoreValue; } }
    public bool MovingState { get { return mMovingState; } set { mMovingState = value; } }


    // Gets components and sets private variables
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weapons = GetComponents<Weapon>();
        movement = new Vector2(1, 0) * staticSpeed * Time.fixedDeltaTime;
    }

    // Finds player
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Updates the vector pointing towards the player and shoots if the enemy is in movingstate
    private void Update()
    {
        if (target != null)
        {
            vectorToTarget = new Vector2(target.transform.position.x - this.transform.position.x, -1);
            vectorToTarget = vectorToTarget / vectorToTarget.magnitude;
            vectorToTarget = vectorToTarget * movingSpeed * Time.fixedDeltaTime;
            vectorToTarget.x *= XSpeedMultiplier;
        }

        if (MovingState)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].Shoot();
            }
        }
    }

    // Physics based movement
    private void FixedUpdate()
    {
        Move();
    }

    // Loops the enemy if it hits the trigger below the map
    // Could also just check the y position of the enemy to do this
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Loop")
        {
            transform.position = new Vector2(transform.position.x, 7);
        }
    }


    // Function for making the enemy change direction
    public void ChangeDirection()
    {
        movement = -movement;
    }

    // Changes the state of enemy to moving
    public void ChangeState()
    {
        MovingState = true;
    }


    // Moves the enemy based on its state
    private void Move()
    {
        if (!MovingState)
            rb.MovePosition(transform.position + movement);

        else if (MovingState)
        {
            if (initialMove) // Used for the little loop at the beginning of the movement state
            {
                rb.AddForce(Vector2.up * 3000 * Time.fixedDeltaTime);
                rb.AddForce(Vector2.right * 3000 * Time.fixedDeltaTime);

                initialMove = false;
            }

            rb.AddForce(vectorToTarget);
        }
    }


}
