using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] int hp;
    private Transform enemySpawner;

    Rigidbody2D rb;

    private Vector3 movement;

    void Start()
    {
        enemySpawner = GetComponent<Transform>();
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

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
            Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (Transform enemy in enemySpawner)
        {
            movement = -movement;
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
