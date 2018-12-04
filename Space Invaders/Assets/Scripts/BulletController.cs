using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    // Inspector variables
    [SerializeField] float speed;
    [SerializeField] int dmg;

    // Private variables
    Rigidbody2D rb;

    // Gets components
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Used to add force to the bullet on it's spawn
    void Start () {
        Move();
    }

    // Adds a force based on speed the 'up' vector to the bullet
    private void Move()
    {
        rb.AddForce(Vector2.up * speed * Time.fixedDeltaTime);
    }

    // Damages enemies and destroys the bullet on impact
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Checks if hit object has a enemycontroller component, if it does damage the gameobject

        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(dmg);
        }

        Destroy(this.gameObject);
    }

}
