using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] int hp;
    Rigidbody2D rb;

    public Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2(1, 0) * speed * Time.fixedDeltaTime;
    }

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
        {
            Die();
        }
    }

    public void ChangeDirection()
    {
        movement = -movement;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
