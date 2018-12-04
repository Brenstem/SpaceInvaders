using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Inspector variables 
    [SerializeField] float staticSpeed;
    [SerializeField] float movingSpeed;
    [SerializeField] int hp;
    [SerializeField] float movingStateTimer;

    // Private variables
    private Rigidbody2D rb;
    private Vector3 movement;
    private GameObject target;
    private Vector2 targetVector;
    private float timer = 0;
    private bool movingState = false;

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
        targetVector = target.transform.position;
        timer += Time.deltaTime;
        if (timer > movingStateTimer)
            movingState = true;
    }

    // Physics based movement
    private void FixedUpdate()
    {
        Move();
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

    // Moves the player based on the movement vector
    private void Move()
    {
        if (!movingState)
        {
            rb.MovePosition(transform.position + movement);
        }
        else if (movingState)
        {
            Debug.Log(target.transform.position);
            rb.AddForce(targetVector * Time.deltaTime);
        }
    }

    // Function for killing the player
    void Die()
    {
        Destroy(this.gameObject);
    }
}
