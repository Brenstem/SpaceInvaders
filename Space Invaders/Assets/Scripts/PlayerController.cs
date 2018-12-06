using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Insector variables
    [SerializeField] float speed;

    // Private variables
    private Rigidbody2D rb;
    private Weapon[] weapons; 
    private Vector2 viewportSize;
    private float xBounds;

    // Gets components
    private void Awake()
    {
        Camera camera = Camera.main;
        weapons = GetComponents<Weapon>();

        rb = GetComponent<Rigidbody2D>();
        viewportSize.x = camera.orthographicSize * camera.aspect;
        xBounds = viewportSize.x  - (this.GetComponent<BoxCollider2D>().size.x * this.transform.localScale.x) / 2;
    }

    // Shooting
    private void Update()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Shoot();
        }
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

        if (this.transform.position.x > xBounds)
            this.transform.position = new Vector2(xBounds, this.transform.position.y);

        if (this.transform.position.x < -xBounds)
            this.transform.position = new Vector2(-xBounds, this.transform.position.y);

        rb.MovePosition(transform.position + movement);
    }
}
