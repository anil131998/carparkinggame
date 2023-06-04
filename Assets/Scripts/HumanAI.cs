using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanAI : MonoBehaviour
{
    [SerializeField] private GameObject waypointCollection;
    
    private Transform[] waypoints;
    private NavMeshAgent aIAgent;
    private Animator animator;
    private Rigidbody aiHumanRb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        aIAgent = GetComponent<NavMeshAgent>();
        aiHumanRb = GetComponent<Rigidbody>();

        waypoints = new Transform[waypointCollection.transform.childCount];
        for (int i=0; i<waypoints.Length; i++)
        {
            waypoints[i] = waypointCollection.transform.GetChild(i);
        }

        aIAgent.SetDestination(waypoints[0].transform.position);
    }

    private void Update()
    {
        Vector3 velocity = aiHumanRb.velocity;
        Vector3 localVel = transform.InverseTransformDirection(velocity);

        if (aIAgent.remainingDistance < 1)
        {
            aIAgent.SetDestination(
                waypoints[Random.Range(0, waypoints.Length - 1)].transform.position
            );
        }
        animator.SetBool("isWalking", !(localVel.normalized.magnitude < 0.1f));
    }

}
