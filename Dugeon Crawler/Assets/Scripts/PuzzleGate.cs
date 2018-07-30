using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGate : MonoBehaviour {

    public Animator gate;

	// Use this for initialization
	void Start () {
        //Make sure the gates are closed to start out with
        gate.SetTrigger("Lower");
	}
}
