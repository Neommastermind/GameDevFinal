using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GameUI gameUI;

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
    private static int armor;
    private static int healthPotions;
    private static int staminaRegen;
    //Shield stability 0.0f - 1.0f
    private static float stability;
    private static bool isLevelApplied = true;
    private static bool isDead = false;

    private static Animator weapon;
    private static Animator shield;

    // Use this for initialization
    void Start () {
        strength = 1;
        vitality = 1;
        endurance = 1;
        level = 1;
        weaponDamage = 25;
        stability = 0.15f;

        SetStats();
        health = healthTotal;
        stamina = staminaTotal;
        healthPotions = 3;

        gameUI.UpdateHealthTotal();
        gameUI.UpdateStaminaTotal();
        gameUI.UpdateHealth();
        gameUI.UpdateStamina();
        gameUI.UpdateHealthPotions();
        gameUI.UpdateArmor();

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

        if(Input.GetKeyDown(KeyCode.Q) && healthPotions > 0)
        {
            //Drink a health potion
            healthPotions--;
            health += (int)Mathf.Floor(healthTotal * 0.6f);
            health = Mathf.Clamp(health, 0, healthTotal);
            gameUI.UpdateHealthPotions();
        }

        gameUI.UpdateHealth();
        gameUI.UpdateStamina();
    }

    IEnumerator RegenStamina()
    {
        while (!isDead)
        {
            if (!shield.GetBool("Blocking") && stamina < staminaTotal)
            {
                //Only regen stamina if the player isn't blocking, and you haven't exceeded the stamina total.
                stamina += staminaRegen;
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
        expNeeded = level*100 + (int)Mathf.Floor(Mathf.Exp(level));
        fullDamage = weaponDamage + (5 * (strength-1));
        staminaRegen = 5 + (endurance * 5);
    }

    IEnumerator Die()
    {
        isDead = true;
        enabled = false;
        Destroy(weapon.gameObject);
        Destroy(shield.gameObject);
        gameUI.DisplayDeathScreen();
        yield return new WaitForSeconds(5);
        gameUI.LoadMainMenu();
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
                StartCoroutine("Die");
            }
            else
            {
                //animator.Play("Damaged");
            }
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetStamina()
    {
        return stamina;
    }

    public int GetHealthPotions()
    {
        return healthPotions;
    }

    public int GetArmor()
    {
        return armor;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetWeaponDamage()
    {
        return weaponDamage;
    }

    public float GetShieldStability()
    {
        return stability;
    }

    public int GetFullDamage()
    {
        return fullDamage;
    }

    public int GetExp()
    {
        return exp;
    }

    public int GetExpNeeded()
    {
        return expNeeded;
    }

    public int GetStrength()
    {
        return strength;
    }

    public int GetVitality()
    {
        return vitality;
    }

    public int GetHealthTotal()
    {
        return healthTotal;
    }

    public int GetEndurance()
    {
        return endurance;
    }

    public int GetStaminaTotal()
    {
        return staminaTotal;
    }

    public int GetStaminaRegen()
    {
        return staminaRegen;
    }

    public int GetGold()
    {
        return gold;
    }

    public void AddExp(int experience)
    {
        exp += experience;

        if (isLevelApplied && exp >= expNeeded)
        {
            isLevelApplied = false;
            gameUI.DisplayRequest("Level Up!");
            gameUI.ShowUpgrades();
        }
    }

    public void AddGold(int goldGain)
    {
        gold += goldGain;
    }

    public void AddHealthPotions(int potions)
    {
        healthPotions += potions;
        gameUI.UpdateHealthPotions();
        gameUI.DisplayRequest("You have recieved " + potions + " Health Potions!");
    }

    public void AddWeaponDamage(int damage)
    {
        weaponDamage += damage;
        fullDamage = weaponDamage + (5 * (strength - 1));
        gameUI.DisplayRequest("You have recieved a better weapon!");
    }

    public void AddArmor(int armorAmount)
    {
        armor += armorAmount;
        gameUI.UpdateArmor();
        gameUI.DisplayRequest("You have recieved " + armor + " Armor!");
    }

    public void AddShieldStability(float shieldStability)
    {
        stability += shieldStability;
        stability = Mathf.Clamp(stability, 0.0f, 1.0f);
        gameUI.DisplayRequest("You have recieved a better shield!");
    }

    public void incrementStrength()
    {
        if (!isLevelApplied)
        {
            level++;
            strength++;
            exp = exp - expNeeded;
            isLevelApplied = true;
            SetStats();
        }
    }

    public void incrementVitality()
    {
        if (!isLevelApplied)
        {
            vitality++;
            level++;
            exp = exp - expNeeded;
            isLevelApplied = true;
            SetStats();
        }
    }

    public void incrementEndurance()
    {
        if (!isLevelApplied)
        {
            endurance++;
            level++;
            exp = exp - expNeeded;
            isLevelApplied = true;
            SetStats();
        }
    }

}
