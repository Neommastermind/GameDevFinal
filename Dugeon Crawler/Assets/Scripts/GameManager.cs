﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject[] zones;

    private int zone = 0;
    private int enemyTotal = 0;
    private int enemiesDefeated = 0;

    // Use this for initialization
    void Start () {
        SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnEnemies()
    {
        //Retrieve the spawn points for the current zone
        Transform[] spawners = FindGameObjectInChildWithTag(zones[zone], "Spawners").GetComponentsInChildren<Transform>();
        //Set the total amount of enemies to the amount of spawners
        enemyTotal = spawners.Length - 1;

        //Spawn a single enemy at every spawn point in this zone
        for(int i = 1; i < spawners.Length; i++)
        {
            Instantiate(enemy, spawners[i].position, spawners[i].rotation);
        }
    }

    public void NextZone()
    {
        //Set up the game for the next zone
        zone++;
        SpawnEnemies();
        //Make sure the zone gates are closed
        CloseZoneGates();
    }

    private void CloseZoneGates()
    {
        //Close the exit gate
        FindGameObjectInChildWithTag(zones[zone], "Exit").GetComponentInChildren<Animator>().SetTrigger("Lower");
        //Close the treasure gates
        FindGameObjectInChildWithTag(zones[zone], "Treasure").GetComponentInChildren<Animator>().SetTrigger("Lower");
    }

    private void OpenZoneGates()
    {
        //Open the exit gate
        FindGameObjectInChildWithTag(zones[zone], "Exit").GetComponentInChildren<Animator>().SetTrigger("Raise");
        //Open the treasure gates
        FindGameObjectInChildWithTag(zones[zone], "Treasure").GetComponentInChildren<Animator>().SetTrigger("Raise");
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        if(enemiesDefeated == enemyTotal)
        {
            //Open the gates!
            OpenZoneGates();
            //Reset the counter
            enemiesDefeated = 0;
        }
    }

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        //Loop through all of the transforms to find the right child
        foreach(Transform transform in parent.transform)
        {
            if(transform.CompareTag(tag))
            {
                return transform.gameObject;
            }
        }
        //Return null if we don't find anything
        return null;
    }
}
