using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
    public GameObject minion;
    public GameObject boss;
    public GameObject[] zones;

    private int zone = 0;
    private int enemyTotal = 0;
    private int enemiesDefeated = 0;
    private bool isBoss;
    private bool isBossStarted;
    private List<Enemy> minions;
    private List<Transform> minionSpawners;

    // Use this for initialization
    void Start () {
        //SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnEnemies()
    {
        //Retrieve the spawn points for the current zone
        Transform[] spawners = FindGameObjectInChildWithTag(zones[zone], "Spawners").GetComponentsInChildren<Transform>();
        //Set the total amount of enemies to the amount of spawners
        //Subtract one, since the Spawners object is also returned
        enemyTotal = spawners.Length - 1;
        //Make sure to reset this just in case
        enemiesDefeated = 0;

        if(isBoss)
        {
            minions = new List<Enemy>();
            minionSpawners = new List<Transform>();
        }

        //Spawn a single enemy at every spawn point in this zone
        for(int i = 1; i < spawners.Length; i++)
        {
            Transform current = spawners[i];
            //Spawn the right enemies
            if (current.CompareTag("BossSpawner"))
            {
                Instantiate(boss, current.position, current.rotation);
            }
            else if (current.CompareTag("MinionSpawner"))
            {
                minions.Add(Instantiate(minion, current.position, current.rotation).GetComponent<Enemy>());
                minionSpawners.Add(current);
            }
            else
            {
                Instantiate(enemy, current.position, current.rotation);
            }
        }
    }

    public void NextZone()
    {
        //Set up the game for the next zone
        zone++;

        //Check to see if the zone is a boss fight
        if (zones[zone].CompareTag("BossArena"))
        {
            isBoss = true;
        }
        else if(!zones[zone].CompareTag("FloorExit"))
        {
            //Spawn all of the intital enemies for the zone
            SpawnEnemies();
        }

        //Make sure the zone gates are closed
        CloseZoneGates();
    }

    public void StartBossFight()
    {
        isBossStarted = true;
        //Spawn all of the intital enemies for the zone
        SpawnEnemies();
        //Start spawning minions if this is a boss zone
        StartCoroutine("SpawnMinions");
    }

    private void CloseZoneGates()
    {
        //Close the exit gate
        GameObject gameObjectExit = FindGameObjectInChildWithTag(zones[zone], "Exit");
        if (gameObjectExit != null)
        {
            Animator exit = gameObjectExit.GetComponentInChildren<Animator>();
            exit.SetTrigger("Lower");
            exit.gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(SoundManager.Instance.gateClose);
        }
        //Close the treasure gates
        GameObject gameObjectTreasure = FindGameObjectInChildWithTag(zones[zone], "Treasure");
        if (gameObjectTreasure != null)
        {
            Animator treasure = gameObjectTreasure.GetComponentInChildren<Animator>();
            treasure.SetTrigger("Lower");
            treasure.gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(SoundManager.Instance.gateClose);
        }
    }

    private void OpenZoneGates()
    {
        //Open the exit gate
        GameObject gameObjectExit = FindGameObjectInChildWithTag(zones[zone], "Exit");
        if (gameObjectExit != null)
        {
            Animator exit = gameObjectExit.GetComponentInChildren<Animator>();
            exit.SetTrigger("Raise");
            exit.gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(SoundManager.Instance.gateOpen);
        }
        //Open the treasure gates
        GameObject gameObjectTreasure = FindGameObjectInChildWithTag(zones[zone], "Treasure");
        if (gameObjectTreasure != null)
        {
            Animator treasure = gameObjectTreasure.GetComponentInChildren<Animator>();
            treasure.SetTrigger("Raise");
            treasure.gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(SoundManager.Instance.gateOpen);
        }
    }

    public void MinionDefeated(Enemy enemy)
    {
        //Remove the minion from the list
        minions.Remove(enemy);
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

    public void BossDefeated()
    {
        //We are no longer in a boss fight
        isBoss = false;
        isBossStarted = false;

        //Kill all of the minions
        foreach(Enemy enemy in minions)
        {
            enemy.Kill();
        }

        //Open the gates
        OpenZoneGates();
    }

    IEnumerator SpawnMinions()
    {
        while(isBoss)
        {
            if(minions.Count < enemyTotal - 1)
            {
                Transform spawner = minionSpawners[Random.Range(0, minionSpawners.Count - 1)];
                minions.Add(Instantiate(minion, spawner.position, spawner.rotation).GetComponent<Enemy>());
            }

            yield return new WaitForSeconds(10);
        }
    }

    public bool GetIsBoss()
    {
        return isBoss;
    }

    public bool GetIsBossStarted()
    {
        return isBossStarted;
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
