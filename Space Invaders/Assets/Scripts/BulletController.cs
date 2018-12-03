using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField] float speed;
    [SerializeField] int dmg;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        Move();
    }

    private void Move()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        EnemyController enemy = hitInfo.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(dmg);
        }

        Destroy(this.gameObject);
    }

}
