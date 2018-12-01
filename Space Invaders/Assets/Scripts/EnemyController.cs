using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] float speed;
    Rigidbody2D rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(1, 0) * speed * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.MovePosition(transform.position + movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movement = -movement;
    }
}
