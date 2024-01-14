using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    UnityEngine.AI.NavMeshAgent agent;
    Projectile projectile;
    public float shootingSpeed = 10f;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    private int ItemCharge = 3;
    void Start() {

    }
    
    void Update() {
        if (Input.GetButtonDown("Jump") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;   
            ItemCharge--;
            Create();
        }
    }

    void Create()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = transform.forward * shootingSpeed;
    }


}
