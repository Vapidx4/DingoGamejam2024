using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    EnemyBehavior behavior;
    private GameObject Player;
    private float detectionAngle;
    private float targetDist;

    // Start is called before the first frame update
    void Start()
    {
        // Get the angle
        behavior = GetComponent<EnemyBehavior>();
        
        if (behavior != null)
        {
            detectionAngle = behavior.angle;
            Player = behavior.player;
            targetDist = behavior.maxDistance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checking for player
        if (Player != null)
        {
            Vector3 distToPlayer = Player.transform.position- transform.position;
            float angle = Vector3.Angle(transform.forward, distToPlayer);

            if (angle < detectionAngle)
            {
                RaycastHit hit;
                Debug.Log("Saw player in angle");
                if (Physics.Raycast(transform.position, distToPlayer.normalized, out hit, targetDist))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject == Player.gameObject)
                    {
                        Debug.Log("GOcha !!!");//---------------------------------- Remove at some point
                        behavior.isAggro = true;
                    }
                }
            }
        } else
        {
            behavior.isAggro = false;
        }
    }
}
