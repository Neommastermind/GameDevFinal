using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetComponentInParent<Animator>().SetTrigger("Lower");
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
