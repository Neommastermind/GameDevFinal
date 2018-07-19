using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity {

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
    private static int armor;
    //Shield stability 0.0f - 1.0f
    private static float stability;

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
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetStats()
    {
        healthTotal = 100 + (10 * (vitality - 1));
        staminaTotal = 100 + (10 * (endurance - 1));
        expNeeded = 100 + (int)Mathf.Floor(Mathf.Exp(level));
        fullDamage = weaponDamage + (10 * (strength-1));

    }
}
