using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Inspector variables 
    [SerializeField] float speed;
    [SerializeField] int hp;

    // Private variables
    private Rigidbody2D rb;
    private Vector3 movement;

    // Gets components and sets private variables
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(1, 0) * speed * Time.fixedDeltaTime;
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
        rb.MovePosition(transform.position + movement);
    }

    // Function for killing the player
    void Die()
    {
        Destroy(this.gameObject);
    }
}
