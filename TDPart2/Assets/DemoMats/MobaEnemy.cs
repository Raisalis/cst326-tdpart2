using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobaEnemy : MonoBehaviour
{
    public Transform target;

    public int health = 3;
    public int coins = 2;
    public int power = 1;
    public Base playerBase;

    public List<Transform> waypointList;
    private int targetWaypointIndex;
    private Vector3 currentDirection;
    public Manager manager;
    public GameObject healthbar;
    private int inithealth;
    private float inithealthbar;

    // public delegate void EnemyDied(EnemyComplete deadEnemy);
    // public event EnemyDied OnEnemyDied;

    // Create and get a reference to the NavMeshAgent
    private UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        inithealth = health;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        targetWaypointIndex = 1;

        // Place enemy at closest navmesh point (create GetNavmeshPosition)
        Vector3 meshPosition = GetNavmeshPosition(waypointList[1].position);
        agent.SetDestination(meshPosition);

        // Healthbar object and value setup
        Transform childbar = gameObject.transform.Find("Healthbar");
        healthbar = childbar.gameObject;
        inithealthbar = healthbar.transform.localScale.y;

        GameObject temp = GameObject.Find("Base");
        playerBase = temp.GetComponent<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 meshPosition = GetNavmeshPosition(waypointList[targetWaypointIndex].position);
        agent.SetDestination(meshPosition);

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
            playerBase.removeHealth(power);
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
        healthbar.transform.localScale = new Vector3(healthbar.transform.localScale.x, (float)health/(float)inithealth * inithealthbar, healthbar.transform.localScale.z);
        if(health <= 0) {
            manager.addCoins(coins);
            Destroy(this.gameObject);
        }
    }
}
