using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Text healthText;
    public Text staminaText;

    private static int health;
    private static int stamina;
    private static int healthTotal;
    private static int staminaTotal;
    private static int gold;
    private static int exp;
    private static int expNeeded;
    private static int level;
    private static int strength;
    private static int vitality;
    private static int endurance;
    private static int weaponDamage;
    private static int fullDamage;
    private static int armor = 0;
    //Shield stability 0.0f - 1.0f
    private static float stability = 0.05f;
    private static bool isLevelApplied = true;
    private static bool isDead = false;

    private static Animator weapon;
    private static Animator shield;

    // Use this for initialization
    void Start () {
        if (strength <= 0)
            strength = 1;
        if (vitality <= 0)
            vitality = 1;
        if (endurance <= 0)
            endurance = 1;
        if (level <= 0)
            level = 1;
        if (weaponDamage <= 0)
            weaponDamage = 25;

        SetStats();
        health = healthTotal;
        stamina = staminaTotal;

        healthText.text = "Health: " + health;
        staminaText.text = "Stamina: " + stamina;

        weapon = GameObject.FindGameObjectWithTag("Sword").GetComponent<Animator>();
        shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Animator>();

        StartCoroutine("RegenStamina");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) && stamina >= 50 && weapon.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //Heavy attack
            stamina -= 50;
            weapon.Play("Heavy-Attack");
        }
        else if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && stamina >= 25 && weapon.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            //Light attack
            stamina -= 25;
            weapon.Play("Light-Attack");
        }

        if(Input.GetMouseButtonDown(1))
        {
            //Start blocking
            shield.SetBool("Blocking", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            //Stop Blocking
            shield.SetBool("Blocking", false);
        }

        healthText.text = "Health: " + health;
        staminaText.text = "Stamina: " + stamina;
    }

    IEnumerator RegenStamina()
    {
        while (!isDead)
        {
            if (!shield.GetBool("Blocking") && stamina < staminaTotal)
            {
                //Only regen stamina if the player isn't blocking, and you haven't exceeded the stamina total.
                stamina += 5 + (endurance*5);
                //Make sure we don't go over the stamina total.
                stamina = Mathf.Clamp(stamina, 0, staminaTotal);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void SetStats()
    {
        healthTotal = 100 + (10 * (vitality - 1));
        staminaTotal = 100 + (10 * (endurance - 1));
        expNeeded = 100 + (int)Mathf.Floor(Mathf.Exp(level));
        fullDamage = weaponDamage + (5 * (strength-1));

    }

    public void TakeDamage(int damage, bool blocked)
    {
        if (!isDead)
        {
            if (!blocked)
            {
                health -= Mathf.Abs(damage - armor);
            }
            else
            {
                int staminaDamage = (int)Mathf.Floor(stability * damage);
                if (stamina - staminaDamage < 0)
                {
                    health -= Mathf.Abs(damage - stamina - armor);
                    stamina = 0;
                    
                }
                else {
                    health -= Mathf.Abs(damage - staminaDamage - armor);
                    stamina -= staminaDamage;
                }
            }

            health = Mathf.Clamp(health, 0, healthTotal);

            if (health == 0)
            {
                isDead = true;
                Debug.Log("Dead");
            }
            else
            {
                //animator.Play("Damaged");
            }
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetFullDamage()
    {
        return fullDamage;
    }

    public void AddExp(int experience)
    {
        exp += experience;

        if (isLevelApplied && exp >= expNeeded)
        {
            isLevelApplied = false;

        }
    }

    public void AddGold(int goldGain)
    {
        gold += goldGain;
    }
}
