using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // Inspector variables 
    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;

    // private variables
    private float fireTimer = 0;

    // Instantiates bullets at firepoints if it's been long enough since the last shot
    public void Shoot()
    {
        fireTimer += Time.deltaTime;
        float fire = Input.GetAxisRaw("Fire1");

        if (fire == 1 && fireTimer > fireRate)
        {
            Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation, this.transform);
            Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation, this.transform);
            fireTimer = 0;
        }
    }
}
