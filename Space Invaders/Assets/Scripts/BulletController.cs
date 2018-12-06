using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    // Inspector variables
    [SerializeField] float speed;
    [SerializeField] int dmg;

    // Private variables
    private Rigidbody2D rb;
    private Vector2 shotDirection;

    // Gets components
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Used to add force to the bullet on it's spawn
    void Start ()
    {
        if (transform.parent.gameObject.CompareTag("Player"))
            shotDirection = Vector2.up;
        else if (transform.parent.gameObject.CompareTag("Enemy"))
            shotDirection = Vector2.down;

        Move();
    }

    // Adds a force based on speed the 'up' vector to the bullet
    private void Move()
    {
        rb.AddForce(shotDirection * speed * Time.fixedDeltaTime);
    }

    // Damages enemies and destroys the bullet on impact
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Checks if hit object has a enemycontroller component, if it does damage the gameobject
        if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Enemy"))
            hitInfo.GetComponent<Health>().TakeDamage(dmg);

        if(hitInfo.tag != transform.parent.tag)
            Destroy(this.gameObject);
    }

}
