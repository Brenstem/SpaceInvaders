using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] Transform firePoint1;
    [SerializeField] Transform firePoint2;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;
    float fireTimer;

    private void Start()
    {
        fireTimer = 0;
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        fireTimer += Time.deltaTime;
        float fire = Input.GetAxisRaw("Fire1");

        if (fire == 1 && fireTimer > fireRate)
        {
            Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
            Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            fireTimer = 0;
        }
    }
}
