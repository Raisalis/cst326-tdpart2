using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Popup : MonoBehaviour
{

    public GameObject ui;
    private GameObject placement;
    public GameObject tower1prefab;
    public Manager manager;

    public TMP_Text costText;
    public Button buildButton;
    public Button cancelButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = buildButton.GetComponent<Button>();
        btn.onClick.AddListener(buildTask);
        Button cbtn = cancelButton.GetComponent<Button>();
        cbtn.onClick.AddListener(cancelTask);
        costText.text = String.Format("Cost: {0:00} Coins", tower1prefab.GetComponent<Tower>().getCost());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(GameObject incomingPlacement) {
        ui.SetActive(!ui.activeSelf);
        placement = incomingPlacement;
    }

    // Build the tower if enough gold, else error.
    void buildTask() {
        int playerMoney = manager.getPurse();
        if(playerMoney > 0) {
            int towerCost = tower1prefab.GetComponent<Tower>().getCost();
            if(towerCost > playerMoney) {
                Debug.Log("Error: Not enough money to build tower.");
                cancelTask();
            } else {
                GameObject tower = Instantiate(tower1prefab);
                tower.transform.position = placement.transform.position;
                manager.removeCoins(towerCost);
                cancelTask();
            }
        } else {
            Debug.Log("Error: Not enough money to build tower.");
            cancelTask();
        }
    }

    // Exit popup window
    void cancelTask() {
        ui.SetActive(!ui.activeSelf);
    }
}
