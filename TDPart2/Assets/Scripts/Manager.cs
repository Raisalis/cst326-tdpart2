using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Manager : MonoBehaviour
{
    private int coins;
    public List<Transform> waypointList;
    public GameObject enemyPrefab;
    public TMP_Text purseText;

    // Start is called before the first frame update
    void Start()
    {
        coins = 50;
        purseText.text = String.Format("Purse: {0:000} Coins", coins);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Space. Spawn Enemy if it's pressed.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spawnEnemy();
        }
        
    }

    // Function to Spawn Enemy
    public void spawnEnemy()
    {
        // Instantiate Enemy
        GameObject newObject = Instantiate(enemyPrefab);
        // Set Waypoint List
        newObject.transform.position = waypointList[0].position;
        newObject.GetComponent<MobaEnemy>().SetVariables(waypointList, this);
    }

    // Function to add Coins
    public void addCoins(int amount)
    {
        coins += amount;
        // onscreen ui coin count edit
        purseText.text = String.Format("Purse: {0:000} Coins", coins);
        // Debug.Log("Enemy Defeated!\nPurse: " + coins + " coins");
    }

    public void removeCoins(int amount)
    {
        coins -= amount;
        // onscreen ui coin count edit
        purseText.text = String.Format("Purse: {0:000} Coins", coins);
    }

    public int getPurse() {
        int current = coins;
        return current;
    }

}
