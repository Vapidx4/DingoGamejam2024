using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    EnemyBehavior behavior;
    private GameObject Player;
    private float detectionAngle;
    private float targetDist;

    public AudioClip aggroSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        behavior = GetComponent<EnemyBehavior>();

        if (behavior != null)
        {
            detectionAngle = behavior.angle;
            Player = behavior.player;
            targetDist = behavior.maxDistance;
        }

        // Add an AudioSource component to the enemy
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = aggroSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            Vector3 distToPlayer = Player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, distToPlayer);

            if (angle < detectionAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, distToPlayer.normalized, out hit, targetDist))
                {
                    if (hit.collider.gameObject == Player.gameObject)
                    {
                        behavior.isAggro = true;
                        PlayAggroSound();
                    }
                }
            }
        }
        else
        {
            behavior.isAggro = false;
            audioSource.Stop(); // Stop playing the sound when not in aggro state
        }
    }

    void PlayAggroSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
