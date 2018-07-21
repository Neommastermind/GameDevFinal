using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int health;
    private int healthTotal;
    private int gold;
    private int exp;
    private int level;
    private int strength;
    private int vitality;
    private int weaponDamage;
    private int fullDamage;
    private int armor;
    private bool dead = false;

    private Animator animator;
    private Player player;

    // Use this for initialization
    void Start () {
        //Get the animator
        animator = GetComponent<Animator>();

        //Find the player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Get a random level between the players level -2 and the players level + 6 (Exclusive)
        int playerLevel = player.GetLevel();
        level = Random.Range(playerLevel >= 3 ? playerLevel - 2 : playerLevel , playerLevel + 6);

        //Make sure to set the initial vitality and strength to 1
        vitality = 1;
        strength = 1;

        //Randomly set the enemies stats
        for(int i = 0; i < level - 1; i++)
        {
            int selection = Random.Range(0, 2);
            switch(selection)
            {
                case 0:
                    strength += 1;
                    break;
                case 1:
                    vitality += 1;
                    break;
            }
        }

        healthTotal = 100 + (10 * (vitality - 1));
        health = healthTotal;
        weaponDamage = 25;
        fullDamage = weaponDamage + (10 * (strength - 1));
        armor = Random.Range(0, level/3);
        exp = 15*level;
        gold = 100*level;

        //Start attacking
        //StartCoroutine("Attack");
    }
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator Attack()
    {
        while(!dead)
        {
            int selection = Random.Range(0, 2);
            switch(selection)
            {
                case 0:
                    animator.Play("Light-Attack");
                    break;
                case 1:
                    animator.Play("Heavy-Attack");
                    break;
            }

            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator Die()
    {
        player.AddExp(exp);
        player.AddGold(gold);
        animator.Play("Death");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (!dead) {
            health -= (damage - armor);

            if (health <= 0)
            {
                dead = true;
                StartCoroutine("Die");
            }
            else
            {
                animator.Play("Damaged");
            }
        }
    }

    public int GetFullDamage()
    {
        return fullDamage;
    }

}
