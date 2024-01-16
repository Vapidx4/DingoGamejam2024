using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public bool isAggro = false;
    private NavMeshAgent agent;
    public float angle = 45;
    public float maxDistance = 10;
    public float timer = 1.0f;
    public float visionCheckRate = 1.0f;
    public float speed;
    public Transform[] points;
    private int destPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent == null)
        {
            Debug.Log("no agent");
        }
        if (player == null)
        {
            Debug.Log("no player");
        }

        if (isAggro)
        {
            agent.destination = player.transform.position;
            agent.speed = 1.2f * speed;
            isAggro= false;
        }
        else
        {
            if (HasReachedDestination())
            {
                GotoNextPoint();
            }
            agent.speed = speed;
        }
    }

    void GotoNextPoint()
    {
        ResumeMovement();

        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;
        points[destPoint].GetComponent<Renderer>().material.color = Color.red;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    public void ResumeMovement()
    {
        agent.isStopped = false;
    }

    public bool HasReachedDestination()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) < 1.5f)
        {
            return true;
        }
        return false;
    }

    public void StopMovement()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            StopMovement();
            Invoke("ResumeMovement", 2f);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Dead");
            //----------------------------------------------------Game Over UI
        }
    }
}
