using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobaEnemy : MonoBehaviour
{
    public Transform target;

    public int health = 3;
    public int coins = 2;

    public List<Transform> waypointList;
    private int targetWaypointIndex;
    private Vector3 currentDirection;
    public Manager manager;

    // public delegate void EnemyDied(EnemyComplete deadEnemy);
    // public event EnemyDied OnEnemyDied;

    // Create and get a reference to the NavMeshAgent
    private UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        targetWaypointIndex = 1;

        // Place enemy at closest navmesh point (create GetNavmeshPosition)
        Vector3 meshPosition = GetNavmeshPosition(waypointList[1].position);
        agent.SetDestination(meshPosition);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 meshPosition = GetNavmeshPosition(waypointList[targetWaypointIndex].position);
        agent.SetDestination(meshPosition);
        /*
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            // Raycast from the mouse and pick a new destination for the agent
            Ray pickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(pickRay, out RaycastHit hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
        */

        Vector3 targetPosition = waypointList[targetWaypointIndex].position;
        Vector3 currentPosition = transform.position;
        //Debug.Log("Current distance from target: " + Vector3.Distance(currentPosition, targetPosition));

        // Target the next waypoint if it's past the current waypoint
        if(Vector3.Distance(currentPosition, targetPosition) < 10)
        {
            TargetNextWaypoint();
        }
        
    }

    Vector3 GetNavmeshPosition(Vector3 samplePosition)
    {
        // Place enemy at closest navmesh point
        UnityEngine.AI.NavMesh.SamplePosition(samplePosition, out UnityEngine.AI.NavMeshHit hitInfo, 100f, -1);
        return hitInfo.position;
    }

    // Function to target the next waypoint
    private void TargetNextWaypoint()
    {
        targetWaypointIndex += 1;

        // Checks if the enemy reached the base. If so, it's destroyed.
        if(targetWaypointIndex > waypointList.Count - 1) {
            Destroy(this.gameObject);
        } else {
            Vector3 meshPosition = GetNavmeshPosition(waypointList[targetWaypointIndex].position);
            agent.SetDestination(meshPosition);
        }
        
    }

    // Function to set the Waypoint List
    public void SetVariables(List<Transform> incomingList, Manager incomingManager)
    {
        waypointList = incomingList;
        manager = incomingManager;
    }

    // Function to reduce life on hit
    public void hit(int damage)
    {
        health -= damage;
        if(health <= 0) {
            manager.addCoins(coins);
            Destroy(this.gameObject);
        }
    }
}
