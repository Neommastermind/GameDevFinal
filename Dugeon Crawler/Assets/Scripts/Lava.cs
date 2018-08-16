using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //Have the sound manager play the lava death sound if anything hits it
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.lavaDeath);

        if (other.CompareTag("Player"))
        {
            //Get the player object
            Player player = other.GetComponent<Player>();
            //Kill the player
            player.Kill();
        }
        else if (other.CompareTag("Enemy"))
        {
            //Get the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            //Kill the enemy
            enemy.Kill();
        }
    }
}
