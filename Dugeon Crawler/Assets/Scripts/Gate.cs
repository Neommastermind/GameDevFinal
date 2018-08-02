using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //Lower the gate
        if(other.CompareTag("Player"))
        {
            GetComponentInParent<Animator>().SetTrigger("Lower");
            GetComponentInParent<AudioSource>().PlayOneShot(SoundManager.Instance.gateClose);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        //Inform the game manager that we have entered a new zone
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().NextZone();
        
    }
}
