using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Projectile projectilePrefab;
    public float shootingSpeed = 10f;
    public float fireRate = 0.5f;
    Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //what happens
            Debug.Log("test");
            Destroy(this.gameObject);
        }
    }
}
