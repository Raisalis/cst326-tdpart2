using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    
    public int health = 3;
    public float speed = 3f;
    public int coins = 3;

    public List<Transform> waypointList;
    private int targetWaypointIndex;
    private Vector3 currentDirection;
    public Manager manager;

    void Start()
    {
        transform.position = waypointList[0].position;
        targetWaypointIndex = 1;
        Vector3 targetPosition = waypointList[targetWaypointIndex].position;
        currentDirection = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        // Move towards next waypoint
        Vector3 targetPosition = waypointList[targetWaypointIndex].position;
        Vector3 movementDirection = (targetPosition - transform.position).normalized;

        // Target the next waypoint if it's past the current waypoint
        if(Vector3.Dot(movementDirection, currentDirection) < 0)
        {
            transform.position = targetPosition;
            TargetNextWaypoint();
        } else {
            Vector3 newPosition = transform.position;
            newPosition += movementDirection * speed * Time.deltaTime;
            currentDirection = movementDirection;

            transform.position = newPosition;
        }
    }

    // Function to target the next waypoint
    private void TargetNextWaypoint()
    {
        targetWaypointIndex += 1;

        // Checks if the enemy reached the base. If so, it's destroyed.
        if(targetWaypointIndex > waypointList.Count - 1) {
            Destroy(this.gameObject);
        } else {
            Vector3 targetPosition = waypointList[targetWaypointIndex].position;
            currentDirection = (targetPosition - transform.position).normalized;

            Vector3 newPosition = transform.position;
            newPosition += currentDirection * speed * Time.deltaTime;
            transform.position = newPosition;
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
