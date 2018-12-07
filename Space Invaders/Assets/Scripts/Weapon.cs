using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Inspector variables 
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;
    [SerializeField] bool autoFire;

    // private variables
    private float fireTimer = 0;
    private Vector2 firePosition;


    // Updates the position where the bullet spawns
    public void Update()
    {
        firePosition = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
    }

    // Gizmos for the firepoint
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(firePosition, new Vector2(0.1f, 0.1f));
    }


    // Instantiates bullets at firepoint if it's been long enough since the last shot
    public void Shoot()
    {
        fireTimer += Time.deltaTime;
        float fire = Input.GetAxisRaw("Fire1");

        if (!autoFire)
        {
            if (fire == 1 && fireTimer > fireRate)
            {
                Instantiate(bulletPrefab, firePosition, transform.rotation, transform);
                fireTimer = 0;
            }
        }
        else if (autoFire)
        {
            if (fireTimer > fireRate)
            {
                Instantiate(bulletPrefab, firePosition, transform.rotation, transform);
                fireTimer = 0;
            }
        }
        
    }


}
