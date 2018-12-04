using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Insector variables
    [SerializeField] float speed;

    // Private variables
    private Rigidbody2D rb;
    private Weapon weapon;

    // Gets components
    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Shooting
    private void Update()
    {
        weapon.Shoot();   
    }

    // Physics based movement
    private void FixedUpdate ()
    {
        Move();
    }

    // Moves the player based on horizontal axis input and speed
    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector2(moveX, 0) * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
