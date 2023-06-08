using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour
{

    [SerializeField] private GameObject waypointCollection;
    [SerializeField] private int startingWaypointIndex;
    [SerializeField] private float wayPointChangeBuffer = 1f; //1 second delay before each path switch

    [SerializeField]private int currentWayPoint;
    private Transform[] waypoints;
    private NavMeshAgent aIAgent;
    private Rigidbody aicarRb;
    private float wayPointChangeTimer; 

    private void Awake()
    {
        aIAgent = GetComponent<NavMeshAgent>();
        aicarRb = GetComponent<Rigidbody>();

        waypoints = new Transform[waypointCollection.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointCollection.transform.GetChild(i);
        }
        currentWayPoint = startingWaypointIndex;
        transform.position = waypoints[currentWayPoint].position;

        wayPointChangeTimer = wayPointChangeBuffer;

        increaseWaypoint();
        aIAgent.SetDestination(waypoints[currentWayPoint].transform.position);
    }

    private void Update()
    {
        Vector3 velocity = aicarRb.velocity;
        Vector3 localVel = transform.InverseTransformDirection(velocity);

        wayPointChangeTimer -= Time.deltaTime;

        if (aIAgent.remainingDistance < .5f && wayPointChangeTimer<=0)
        {
            increaseWaypoint();
            aIAgent.SetDestination(waypoints[currentWayPoint].transform.position);
            wayPointChangeTimer = wayPointChangeBuffer;
        }
    }

    private void increaseWaypoint()
    {
        if (currentWayPoint < waypoints.Length - 1)
            currentWayPoint++;
        else
            currentWayPoint = 0;
    }

}
