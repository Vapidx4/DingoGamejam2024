using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop the particle emission
            Debug.Log("AMIR IS POOOOOOOOOPOOOOOOOOOOOOOOOOO");
            ParticleSystem particleSystem = GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Stop();
                Destroy(gameObject, 1f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
