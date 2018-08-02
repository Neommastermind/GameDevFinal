using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    private Player player;
    public Animator gate;
    public Canvas infoCanvas;
    private bool triggered = false;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audio = GetComponentInParent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if((transform.position - player.transform.position).magnitude <= 4)
        {
            if (!infoCanvas.isActiveAndEnabled)
            {
                infoCanvas.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!triggered)
                {
                    triggered = true;
                    GetComponent<Animator>().SetTrigger("Down");
                    audio.PlayOneShot(SoundManager.Instance.gateOpen);
                    gate.SetTrigger("Raise");
                }
                else
                {
                    triggered = false;
                    GetComponent<Animator>().SetTrigger("Up");
                    audio.PlayOneShot(SoundManager.Instance.gateClose);
                    gate.SetTrigger("Lower");
                }
            }
        }
        else
        {
            if (infoCanvas.isActiveAndEnabled)
            {
                infoCanvas.gameObject.SetActive(false);
            }
        }
	}
}
