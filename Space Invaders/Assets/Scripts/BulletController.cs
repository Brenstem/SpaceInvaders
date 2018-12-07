using UnityEngine;

public class BulletController : MonoBehaviour
{

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

    // Checks whether a player or an enemy is shooting and gives the bullet the according movement direction
    private void Start()
    {
        if (transform.parent.gameObject.CompareTag("Player"))
            shotDirection = Vector2.up;

        else if (transform.parent.gameObject.CompareTag("Enemy"))
            shotDirection = Vector2.down;

        Move();
    }

    // If the hit object has a health component and isn't on the same team as the parent of the bullet it will damage the hit object. 
    // If the object is a wall it'll only destroy the bullet
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<Health>() != null && hitInfo.tag != transform.parent.tag)
        {
            hitInfo.GetComponent<Health>().TakeDamage(dmg);
            Destroy(this.gameObject);
        }
        else if (hitInfo.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

    }


    // Adds a force based on speed and shotdirection to the bullet
    private void Move()
    {
        rb.AddForce(shotDirection * speed * Time.fixedDeltaTime);
    }



}
