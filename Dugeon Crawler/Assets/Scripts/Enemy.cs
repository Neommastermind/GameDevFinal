using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
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
    private bool hasHit = false;
    private bool attacking = false;

    private float navigationTime = 0.0f;
    private float navigationUpdate = 0.25f;
    private Transform target;
    private bool tracking = false;

    private Animator animator;
    private Player player;
    private NavMeshAgent agent;

    public float detectionRange;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //Find the player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Set our navigation target to the player
        target = player.transform;

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
        weaponDamage = 10;
        fullDamage = weaponDamage + (5 * (strength - 1));
        armor = Random.Range(0, level/3);
        exp = 15*level;
        gold = 100*level;

        //Start attacking
        StartCoroutine("Attack");
    }
	
	// Update is called once per frame
	void Update () {
        navigationTime += Time.deltaTime;
        if (navigationTime > navigationUpdate && !attacking)
        {
            //Don't track the target until they are within a certain distance
            if (target != null && (transform.position - target.position).magnitude <= detectionRange)
            {
                if(agent.velocity.magnitude == 0)
                {
                    animator.SetTrigger("Standing");
                }
                else if(agent.velocity.magnitude < 5)
                {
                    animator.SetTrigger("Walking");
                }
                else
                {
                    animator.SetTrigger("Running");
                }

                tracking = true;
                agent.destination = target.position;
            }
            else
            {
                tracking = false;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
                {
                    animator.SetTrigger("Idleing");
                }
            }
            navigationTime = 0;
        }
    }

    private void FixedUpdate()
    {
        //Make sure the enemy is facing the right way during combat
        if (!attacking && tracking && agent.remainingDistance <= agent.stoppingDistance)
        {
            //Set the location where we want to look and make sure we look forward
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;

            //Perform the look
            Quaternion lookRotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10.0f * Time.deltaTime);
        }
    }

    IEnumerator Attack()
    {
        while(!dead)
        {
            if ((transform.position - target.position).magnitude <= 6.0f) {
                int selection = Random.Range(0, 2);
                //Inform the gameobject that we are now attacking
                attacking = true;

                switch (selection)
                {
                    case 0:
                        animator.Play("Light-Attack");
                        break;
                    case 1:
                        animator.Play("Heavy-Attack");
                        break;
                }
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
            health -= Mathf.Abs(damage - armor);
            health = Mathf.Clamp(health, 0, healthTotal);
            Debug.Log("Enemy Health: " + health);

            if (health == 0)
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

    public void DealDamage(bool blocked)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Light-Attack"))
        {
            player.TakeDamage(fullDamage, blocked);
        }
        else
        {
            player.TakeDamage((int)Mathf.Floor(fullDamage * 1.25f), blocked);
        }
    }

    public void ResetAttack()
    {
        //This is used for an animation event to reset the attack booleans
        //At the end of every attack animation
        hasHit = false;
        attacking = false;
    }
   
    public void SetHasHit(bool status)
    {
        hasHit = status;
    }

    public bool GetHasHit()
    {
        return hasHit;
    }

}
