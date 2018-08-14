using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleporter : MonoBehaviour {

    public Transform link;

    private GameManager gameManager;
    private Player player;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (link != null)
        {
            if (other.CompareTag("Player"))
            {
                //Check to make sure the boss fight isn't started
                if (!gameManager.GetIsBossStarted())
                {
                    //Make sure to start the boss fight if isBoss is true
                    if(gameManager.GetIsBoss())
                    {
                        gameManager.StartBossFight();
                    }

                    player.transform.position = link.position;
                    player.transform.rotation = link.rotation;
                }
            }
        }
    }
}
