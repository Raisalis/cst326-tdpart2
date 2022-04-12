using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private int coins;
    public List<Transform> waypointList;
    public GameObject enemyPrefab;
    public TMP_Text purseText;
    public float countdown = 10f;
    public int enemyspawncount = 0;

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

        countdown -= Time.deltaTime;
        if(countdown <= 0 && enemyspawncount < 10) {
            spawnEnemy();
            countdown = 1f;
            enemyspawncount++;
        }

        if(enemyspawncount == 10) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            if(enemies.Length == 0) {
                SceneManager.LoadScene("Win");
            }
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
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
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

    public void gameOver() {
        SceneManager.LoadScene("GameOver");
    }

    public void gameWin() {
        SceneManager.LoadScene("Win");
    }

}
