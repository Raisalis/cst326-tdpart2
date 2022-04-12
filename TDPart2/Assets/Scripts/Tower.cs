using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public int cost = 10;
    public int power = 1;
    private float shootTimer = 0f;
    public Transform targetpos;
    public MobaEnemy targetEnemy;
    public float range = 500f;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindTarget", 0f, .5f);
    }

    void FindTarget() {
        //Debug.Log("Finding Target");
        //Debug.Log("Current Target: ", targetpos);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        float closest = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach(GameObject enemy in enemies) {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if(dist < closest) {
                closest = dist;
                closestEnemy = enemy;
            }
        }

        if(closestEnemy != null && closest <= range) {
            targetpos = closestEnemy.transform;
            targetEnemy = closestEnemy.transform.gameObject.GetComponent<MobaEnemy>();
        } else {
            targetpos = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= 1 && targetpos != null) {
            shoot();
            shootTimer = 0f;
        }
    }

    public int getCost() {
        return cost;
    }

    public void shoot() {
        GameObject temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = temp.GetComponent<Bullet>();
        bullet.Fire(targetpos, targetEnemy, power);
    }
}
