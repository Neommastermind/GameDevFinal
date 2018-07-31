using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Player player;

    private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController cameraController;

    void Start()
    {
        cameraController = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (!stats.isActiveAndEnabled)
            {
                OpenStats();
            }
            else
            {
                CloseStats();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!options.isActiveAndEnabled)
            {
                OpenOptions();
            }
            else
            {
                CloseOptions();
            }
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
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
        if (!stats.isActiveAndEnabled)
        {
            OpenStats();
        }
        else
        {
            stats.gameObject.SetActive(false);
            cameraController.mouseLook.lockCursor = true;
            cameraController.enabled = true;
            player.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OpenStats()
    {
        if (stats.isActiveAndEnabled)
        {
            CloseStats();
        }
        else
        {
            options.gameObject.SetActive(false);
            UpdateStats();
            stats.gameObject.SetActive(true);
            cameraController.mouseLook.lockCursor = false;
            cameraController.enabled = false;
            player.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OpenOptions()
    {
        if (options.isActiveAndEnabled)
        {
            CloseOptions();
        }
        else
        {
            stats.gameObject.SetActive(false);
            options.gameObject.SetActive(true);
            cameraController.mouseLook.lockCursor = false;
            cameraController.enabled = false;
            player.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void CloseOptions()
    {
        if (!options.isActiveAndEnabled)
        {
            OpenOptions();
        }
        else
        {
            options.gameObject.SetActive(false);
            cameraController.mouseLook.lockCursor = true;
            cameraController.enabled = true;
            player.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
