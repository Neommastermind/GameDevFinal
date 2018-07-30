using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    private Player player;
    public Animator gate;
    private bool triggered = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position - player.transform.position).magnitude <= 4 && Input.GetKeyDown(KeyCode.E))
        {
            if (!triggered)
            {
                triggered = true;
                GetComponent<Animator>().SetTrigger("Down");
                gate.SetTrigger("Raise");
            }
            else
            {
                triggered = false;
                GetComponent<Animator>().SetTrigger("Up");
                gate.SetTrigger("Lower");
            }
        }
	}
}
