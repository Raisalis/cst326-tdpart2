using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int health = 3;
    public Manager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeHealth(int damage) {
        health -= damage;
        if(health <= 0) {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            onDeath();
            Destroy(this.gameObject);
        }
    }

    public void onDeath() {
        Debug.Log("Base is dead. Game Over!");
        manager.gameOver();
    }
}
