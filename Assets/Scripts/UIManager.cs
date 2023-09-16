using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public Slider healthPenaltyBar;
    public Slider mutateBar;
    public Slider xpBar;
    public TMP_Text healthText;
    public TMP_Text levelText;
    public TMP_Text xpText;
    public TMP_Text mutationText;
    public TMP_Text mutationLevelText;
    public TMP_Text ATKText;
    public TMP_Text RATKText;
    public TMP_Text DEFText;
    public TMP_Text MSText;
    public TMP_Text HRText;
    public TMP_Text MMultText;
    public TMP_Text XPMultText;
    public PlayerStats playerStats;
    public MutationManager mutationManager;
    public LevelManager levelManager;
    public GameObject statsPanel;
    int baseMaxHealth;
    void Awake()
    {
        baseMaxHealth = playerStats.MaxHealth;
    }
    void Update()
    {
        RefreshHealthBar();
        RefreshMutationBar();
        RefreshXPBar();
        RefreshStatsPanel();
    }

    void RefreshHealthBar()
    {
        healthBar.value = (float)playerStats.CurrentHealth / (float)playerStats.MaxHealth;
        // Show health penalty if Max Health is lower than the base Max Health
        if (baseMaxHealth > playerStats.MaxHealth)
        {
            healthBar.value = (float)playerStats.CurrentHealth / (float)baseMaxHealth;
            healthPenaltyBar.value = ((float)baseMaxHealth - (float)playerStats.MaxHealth) / (float)baseMaxHealth;
        }
        else
        {
            healthPenaltyBar.value = 0f;
        }

        healthText.text = playerStats.CurrentHealth + " / " + playerStats.MaxHealth;
        
        //change color between red and green
        //fill.color = Color.Lerp(Color.red, Color.green, (float)playerStats.CurrentHealth / (float)playerStats.MaxHealth);
    }
    void RefreshMutationBar()
    {
        mutateBar.value = (float)mutationManager.mutation / (float)((mutationManager.mutationLevel + 1) * 25);
        mutationText.text = Mathf.RoundToInt(mutationManager.mutation).ToString() + " / " + ((mutationManager.mutationLevel + 1) * 25).ToString();
        mutationLevelText.text = "Mutation " + mutationManager.mutationLevel.ToString();
    }
    void RefreshXPBar()
    {
        xpBar.value = levelManager.exp/levelManager.expToNextLevel;
        xpText.text = Mathf.RoundToInt(levelManager.exp) + " / " + Mathf.RoundToInt(levelManager.expToNextLevel);
        levelText.text = "level " + levelManager.level;
    }
    void RefreshStatsPanel()
    {
        ATKText.text = "Melee Damage : " + playerStats.AttackDamage.ToString();
        RATKText.text = "Ranged Damage : " + playerStats.BowDamage.ToString();
        DEFText.text = "Damage Reduction : " + playerStats.damageReduction.ToString("F2");
        MSText.text = "Movement Speed : " + playerStats.MoveSpeed.ToString("F2");
        HRText.text = "Heal Rate : " + playerStats.healRate.ToString("F2");
        MMultText.text = "Mutation Rate : " + mutationManager.mutationMultiplier.ToString("F2");
        XPMultText.text = "EXP Multiplier : " + levelManager.expMultiplier.ToString("F2");
}
}
