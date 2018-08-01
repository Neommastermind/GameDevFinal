using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {

    public Canvas infoCanvas;
    private Player player;
    private bool open = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!open && (transform.position - player.transform.position).magnitude <= 3)
        {
            if (!infoCanvas.isActiveAndEnabled)
            {
                infoCanvas.gameObject.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                open = true;
                infoCanvas.gameObject.SetActive(false);
                GetComponent<Animator>().Play("ChestOpen");
                StartCoroutine("OpenChest");
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

    IEnumerator OpenChest()
    {
        int selection = Random.Range(0, 4);
        switch (selection)
        {
            case 0:
                player.AddHealthPotions(Random.Range(1, 4));
                break;
            case 1:
                player.AddWeaponDamage(Random.Range(1, 6));
                break;
            case 2:
                player.AddShieldStability(Random.Range(0.025f, 0.05f));
                break;
            case 3:
                player.AddArmor(Random.Range(1, 6));
                break;
        }

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
