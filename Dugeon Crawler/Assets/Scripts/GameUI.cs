using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameUI : MonoBehaviour {

    public Canvas stats;
    public Canvas options;
    public Text healthText;
    public Text staminaText;
    public Text potionText;
    public Text armorText;
    public Text level;
    public Text exp;
    public Text gold;
    public Text weapon;
    public Text shield;
    public Text strength;
    public Text damage;
    public Text vitality;
    public Text maxHp;
    public Text endurance;
    public Text maxStam;
    public Text stamRegen;
    public Button btnSU;
    public Button btnVU;
    public Button btnEU;

    private Player player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

    public void UpdateHealth()
    {
        healthText.text = "Health: " + player.GetHealth();
    }

    public void UpdateStamina()
    {
        staminaText.text = "Stamina: " + player.GetStamina();
    }

    public void UpdatePotions()
    {
        potionText.text = "Health Potions: " + player.GetHealthPotions();
    }

    public void UpdateArmor()
    {
        armorText.text = "Armor: " + player.GetArmor();
    }

    public void CloseStats()
    {
        stats.gameObject.SetActive(false);
    }

    public void OpenStats()
    {
        UpdateStats();
        stats.gameObject.SetActive(true);
    }

    public void OpenOptions()
    {
        options.gameObject.SetActive(true);
    }

    public void CloseOptions()
    {
        options.gameObject.SetActive(false);
    }

    public void UpdateStats()
    {
        level.text = "Level: " + player.GetLevel();
        exp.text = "Experience Points: " + player.GetExp() + "/" + player.GetExpNeeded();
        gold.text = "Gold: " + player.GetGold();
        weapon.text = "Weapon Damage: " + player.GetWeaponDamage();
        shield.text = "Shield Stability: " + player.GetShieldStability() * 100 + "%";
        strength.text = "Strength: " + player.GetStrength();
        damage.text = "Full Damage: " + player.GetFullDamage();
        vitality.text = "Vitality: " + player.GetVitality();
        maxHp.text = "Max Health: " + player.GetHealthTotal();
        endurance.text = "Endurance: " + player.GetEndurance();
        maxStam.text = "Max Stamina: " + player.GetStaminaTotal();
        stamRegen.text = "Stamina Regeneration: " + player.GetStaminaRegen() + "/s";
    }

    public void ShowUpgrades()
    {
        btnSU.gameObject.SetActive(true);
        btnVU.gameObject.SetActive(true);
        btnEU.gameObject.SetActive(true);
    }

    private void CloseUpgrades()
    {
        btnSU.gameObject.SetActive(false);
        btnVU.gameObject.SetActive(false);
        btnEU.gameObject.SetActive(false);
    }

    public void UpgradeStrength()
    {
        player.incrementStrength();
        CloseUpgrades();
        UpdateStats();
    }

    public void UpgradeVitality()
    {
        player.incrementVitality();
        CloseUpgrades();
        UpdateStats();
    }

    public void UpgradeEndurance()
    {
        player.incrementEndurance();
        CloseUpgrades();
        UpdateStats();
    }
}
