using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Popup : MonoBehaviour
{

    public GameObject ui;
    public GameObject tower1ui;
    public GameObject tower2ui;
    private GameObject placement;
    public GameObject tower1prefab;
    public GameObject tower2prefab;
    public Manager manager;

    public TMP_Text costText;
    public Button buildButton;
    public Button cancelButton;
    public Button tower1Button;
    public Button tower2Button;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = buildButton.GetComponent<Button>();
        btn.onClick.AddListener(buildTask);
        Button cbtn = cancelButton.GetComponent<Button>();
        cbtn.onClick.AddListener(cancelTask);
        // NOT WORKING xd
        /*
        Button tower1btn = tower1Button.GetComponent<Button>();
        tower1btn.onClick.AddListener(tower1panelOpen);
        Button tower2btn = tower2Button.GetComponent<Button>();
        tower2btn.onClick.AddListener(tower2panelOpen);
        */
        costText.text = String.Format("Cost: {0:00} Coins", tower2prefab.GetComponent<Tower>().getCost());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(GameObject incomingPlacement) {
        ui.SetActive(!ui.activeSelf);
        placement = incomingPlacement;
        /*
        cancelButton = ui.transform.GetChild(0).GetChild(4).GetComponent<Button>();
        cancelButton.onClick.AddListener(delegate{cancelTask(0);});
        */
    }

    // NOT WORKING
    /*
    void tower1panelOpen() {
        tower1ui.SetActive(!tower1ui.activeSelf);
        costText = tower1ui.transform.GetChild(2).GetComponent<TMP_Text>();
        costText.text = String.Format("Cost: {0:00} Coins", tower1prefab.GetComponent<Tower>().getCost());
        buildButton = tower1ui.transform.GetChild(3).GetComponent<Button>();
        buildButton.onClick.AddListener(delegate{buildTask(1);});
        cancelButton = tower1ui.transform.GetChild(4).GetComponent<Button>();
        cancelButton.onClick.AddListener(delegate{cancelTask(1);});
    }

    // NOT WORKING
    void tower2panelOpen() {
        tower2ui.SetActive(!tower2ui.activeSelf);
        costText = tower2ui.transform.GetChild(2).GetComponent<TMP_Text>();
        costText.text = String.Format("Cost: {0:00} Coins", tower2prefab.GetComponent<Tower>().getCost());
        buildButton = tower2ui.transform.GetChild(3).GetComponent<Button>();
        buildButton.onClick.AddListener(delegate{buildTask(2);});
        cancelButton = tower2ui.transform.GetChild(4).GetComponent<Button>();
        cancelButton.onClick.AddListener(delegate{cancelTask(2);});
    }
    */

    // Build the tower if enough gold, else error.
    void buildTask() {
        int playerMoney = manager.getPurse();
        if(playerMoney > 0) {
            int towerCost = tower2prefab.GetComponent<Tower>().getCost();
            if(towerCost > playerMoney) {
                Debug.Log("Error: Not enough money to build tower.");
                cancelTask();
            } else {
                GameObject tower = Instantiate(tower2prefab);
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
        /*
        if(i == 1) {
            tower1ui.SetActive(!tower1ui.activeSelf);
            cancelButton = ui.transform.GetChild(0).GetChild(4).GetComponent<Button>();
            cancelButton.onClick.AddListener(delegate{cancelTask(0);});
        } else if(i == 2) {
            tower2ui.SetActive(!tower2ui.activeSelf);
            cancelButton = ui.transform.GetChild(0).GetChild(4).GetComponent<Button>();
            cancelButton.onClick.AddListener(delegate{cancelTask(0);});
        } else {
            
        }
        */
        ui.SetActive(!ui.activeSelf);
    }

    /*
    // NOT WORKING
    void closeWindow() {
        ui.SetActive(false);
        tower1ui.SetActive(false);
        tower2ui.SetActive(false);
    }
    */
}
