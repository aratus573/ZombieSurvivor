using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    // we can add Upgrades to this list in inspector
    public List<StatsModifier> listOfAllStatsModifier = new List<StatsModifier>();
    public List<StatsModifier> listOfTempStatsModifier = new List<StatsModifier>();
    public List<StatsModifier> listOfOptionalStatsModifier = new List<StatsModifier>();

    // 3 attributes to be modifiered, so 3 lists to store all modifier numbers
    public List<float> modifiersForMaxHealth = new List<float>{};
    public List<float> modifiersForMoveSpeed = new List<float>{};
    public List<float> modifiersForAttackDamage = new List<float>{};
    public List<float> modifiersForMutationMultiplier = new List<float>{};
    public List<float> modifiersForExpMultiplier = new List<float>{};
    public List<float> modifiersForDamageReduction = new List<float>{};
    public List<float> modifiersForHealRate = new List<float>{};
    public List<float> modifiersForBowDamage = new List<float>{};

    // original value of attributes
    private int baseMaxHealth;
    private float baseMoveSpeed;
    private int baseAttackDamage;
    private float baseMutationMultiplier;
    private float baseExpMultiplier;
    private float baseDamageReduction;
    private float baseHealRate;
    private int baseBowDamage;
    public int numOfOptions = 3;

    public GameObject player;
    public GameObject upgradePanel;
    public GameManager gameManager;
    public UpgradePanelManager upgradePanelManager;
    public MutationManager mutationManager;
    public LevelManager levelManager;

    private PlayerStats playerStats;
    


    private void Awake() 
    {
        playerStats = player.GetComponent<PlayerStats>();

        // get original values of attributes
        baseMaxHealth = playerStats.MaxHealth;
        baseMoveSpeed = playerStats.MoveSpeed;
        baseAttackDamage = playerStats.AttackDamage;
        baseMutationMultiplier = mutationManager.mutationMultiplier;
        baseExpMultiplier = levelManager.expMultiplier;
        baseDamageReduction = playerStats.damageReduction;
        baseHealRate = playerStats.healRate;
        baseBowDamage = playerStats.BowDamage;
        for (int i = 0; i < listOfAllStatsModifier.Count; i++)
        {
            listOfAllStatsModifier[i].applyCount = 0;
            listOfAllStatsModifier[i].isLevelMax = false;
        }
    }

    public void Apply(int whichPlayerChoose)
    {
        StatsModifier _statsModifier = listOfOptionalStatsModifier[whichPlayerChoose];
        string stringModifier1Num =_statsModifier.modifier1.ToString();
        string stringModifier2Num =_statsModifier.modifier2.ToString();
        // for the first attribute
        switch(_statsModifier.targetAtt1.ToString())
        {
            case "MaxHealth":
                // for testing
                Debug.Log("target attributes1 is MaxHealth, modifier is " + stringModifier1Num);

                // add current modifier number to list
                modifiersForMaxHealth.Add(_statsModifier.modifier1);

                // to calculate the sum of whole list, make it a multiplier
                float totalMaxHealthModifiers = 0;
                foreach (var modifiers in modifiersForMaxHealth)
                {
                    totalMaxHealthModifiers += modifiers;
                }
                totalMaxHealthModifiers ++;

                // e.g.: new value = original value * ( 1 + 50% + 30% + -20%...)
                playerStats.maxHealthBeforePenalty = Mathf.RoundToInt(baseMaxHealth * totalMaxHealthModifiers);
                break;

            case "MoveSpeed":
                Debug.Log("target attributes1 is MoveSpeed, modifier is " + stringModifier1Num);
                modifiersForMoveSpeed.Add(_statsModifier.modifier1);
                float totalMoveSpeedModifiers = 0;
                foreach (var modifiers in modifiersForMoveSpeed)
                {
                    totalMoveSpeedModifiers += modifiers;
                }
                totalMoveSpeedModifiers ++;
                playerStats.MoveSpeed = baseMoveSpeed * totalMoveSpeedModifiers;
                break;

            case "AttackDamage":
                Debug.Log("target attributes1 is AttackDamage, modifier is " + stringModifier1Num);
                modifiersForAttackDamage.Add(_statsModifier.modifier1);
                float totalAttackDamageModifiers = 0;
                foreach (var modifiers in modifiersForAttackDamage)
                {
                    totalAttackDamageModifiers += modifiers;
                }
                totalAttackDamageModifiers ++;
                playerStats.AttackDamage = Mathf.RoundToInt(baseAttackDamage * totalAttackDamageModifiers);
                break;

            case "MutationMultiplier":
                Debug.Log("target attributes1 is MutationMultiplier, modifier is " + stringModifier1Num);
                modifiersForMutationMultiplier.Add(_statsModifier.modifier1);
                float totalMutationMultiplierModifiers = 0;
                foreach (var modifiers in modifiersForMutationMultiplier)
                {
                    totalMutationMultiplierModifiers += modifiers;
                }
                mutationManager.mutationMultiplier =totalMutationMultiplierModifiers;
                break;

            case "ExpMultiplier":
                Debug.Log("target attributes1 is ExpMultiplier, modifier is " + stringModifier1Num);
                modifiersForExpMultiplier.Add(_statsModifier.modifier1);
                float totalExpMultiplierModifiers = 0;
                foreach (var modifiers in modifiersForExpMultiplier)
                {
                    totalExpMultiplierModifiers += modifiers;
                }
                levelManager.expMultiplier =totalExpMultiplierModifiers;
                break;

            case "DamageReduction":
                Debug.Log("target attributes1 is DamageReduction, modifier is " + stringModifier1Num);
                modifiersForDamageReduction.Add(_statsModifier.modifier1);
                float totalDamageReductionModifiers = 0;
                foreach (var modifiers in modifiersForDamageReduction)
                {
                    totalDamageReductionModifiers += modifiers;
                }
                playerStats.damageReduction =totalDamageReductionModifiers;
                break;

            case "HealRate":
                Debug.Log("target attributes1 is HealRate, modifier is " + stringModifier1Num);
                modifiersForHealRate.Add(_statsModifier.modifier1);
                float totalHealRateModifiers = 0;
                foreach (var modifiers in modifiersForHealRate)
                {
                    totalHealRateModifiers += modifiers;
                }
                playerStats.healRate =totalHealRateModifiers;
                break;

            case "BowDamage":
                Debug.Log("target attributes1 is BowDamage, modifier is " + stringModifier1Num);
                modifiersForBowDamage.Add(_statsModifier.modifier1);
                float totalBowDamageModifiers = 0;
                foreach (var modifiers in modifiersForBowDamage)
                {
                    totalBowDamageModifiers += modifiers;
                }
                totalBowDamageModifiers ++;
                playerStats.BowDamage = Mathf.RoundToInt(baseBowDamage * totalBowDamageModifiers);
                break;

            case "none":
                Debug.Log("target attributes1 is none");
                break;

            default:
                Debug.Log("it's defalt case");
                break;

        }

        // for the second attribute
        switch(_statsModifier.targetAtt2.ToString())
        {
            case "MaxHealth":
                Debug.Log("target attributes2 is MaxHealth, modifier is " + stringModifier2Num);
                modifiersForMaxHealth.Add(_statsModifier.modifier2);
                float totalMaxHealthModifiers = 0;
                foreach (var modifiers in modifiersForMaxHealth)
                {
                    totalMaxHealthModifiers += modifiers;
                }
                totalMaxHealthModifiers ++;
                playerStats.maxHealthBeforePenalty = Mathf.RoundToInt(baseMaxHealth * totalMaxHealthModifiers);
                break;

            case "MoveSpeed":
                Debug.Log("target attributes2 is MoveSpeed, modifier is " + stringModifier2Num);
                modifiersForMoveSpeed.Add(_statsModifier.modifier2);
                float totalMoveSpeedModifiers = 0;
                foreach (var modifiers in modifiersForMoveSpeed)
                {
                    totalMoveSpeedModifiers += modifiers;
                }
                totalMoveSpeedModifiers ++;
                playerStats.MoveSpeed = Mathf.RoundToInt(baseMoveSpeed * totalMoveSpeedModifiers);
                break;

            case "AttackDamage":
                Debug.Log("target attributes2 is AttackDamage, modifier is " + stringModifier2Num);
                modifiersForAttackDamage.Add(_statsModifier.modifier2);
                float totalAttackDamageModifiers = 0;
                foreach (var modifiers in modifiersForAttackDamage)
                {
                    totalAttackDamageModifiers += modifiers;
                }
                totalAttackDamageModifiers ++;
                playerStats.AttackDamage = Mathf.RoundToInt(baseAttackDamage * totalAttackDamageModifiers);
                break;

            case "MutationMultiplier":
                Debug.Log("target attributes1 is MutationMultiplier, modifier is " + stringModifier2Num);
                modifiersForMutationMultiplier.Add(_statsModifier.modifier2);
                float totalMutationMultiplierModifiers = 0;
                foreach (var modifiers in modifiersForMutationMultiplier)
                {
                    totalMutationMultiplierModifiers += modifiers;
                }
                mutationManager.mutationMultiplier =totalMutationMultiplierModifiers;
                break;

            case "ExpMultiplier":
                Debug.Log("target attributes1 is ExpMultiplier, modifier is " + stringModifier2Num);
                modifiersForExpMultiplier.Add(_statsModifier.modifier2);
                float totalExpMultiplierModifiers = 0;
                foreach (var modifiers in modifiersForExpMultiplier)
                {
                    totalExpMultiplierModifiers += modifiers;
                }
                levelManager.expMultiplier =totalExpMultiplierModifiers;
                break;

            case "DamageReduction":
                Debug.Log("target attributes1 is DamageReduction, modifier is " + stringModifier2Num);
                modifiersForDamageReduction.Add(_statsModifier.modifier2);
                float totalDamageReductionModifiers = 0;
                foreach (var modifiers in modifiersForDamageReduction)
                {
                    totalDamageReductionModifiers += modifiers;
                }
                playerStats.damageReduction =totalDamageReductionModifiers;
                break;

            case "HealRate":
                Debug.Log("target attributes1 is HealRate, modifier is " + stringModifier2Num);
                modifiersForHealRate.Add(_statsModifier.modifier2);
                float totalHealRateModifiers = 0;
                foreach (var modifiers in modifiersForHealRate)
                {
                    totalHealRateModifiers += modifiers;
                }
                playerStats.healRate =totalHealRateModifiers;
                break;
            case "BowDamage":
                Debug.Log("target attributes1 is BowDamage, modifier is " + stringModifier2Num);
                modifiersForBowDamage.Add(_statsModifier.modifier2);
                float totalBowDamageModifiers = 0;
                foreach (var modifiers in modifiersForBowDamage)
                {
                    totalBowDamageModifiers += modifiers;
                }
                totalBowDamageModifiers ++;
                playerStats.BowDamage = Mathf.RoundToInt(baseBowDamage * totalBowDamageModifiers);
                break;

            case "none":
                Debug.Log("target attributes1 is none");
                break;

            default:
                Debug.Log("it's defalt case");
                break;


        }
            
            _statsModifier.applyCount ++;
            
            // after being applied maxApplyCount times, this statsmodifier will be level max
            _statsModifier.isLevelMax = _statsModifier.applyCount >= _statsModifier.maxApplyCount;



    }

    void RandomlyAddToOptionalList(int numOfOptions)
    {
        listOfTempStatsModifier.Clear();
        listOfOptionalStatsModifier.Clear();
        // add statsmodifiers to list of temp
        for (int i = 0; i < listOfAllStatsModifier.Count; i++)
        {
            // only add statsmodifier which isn't level max
            if (!listOfAllStatsModifier[i].isLevelMax)
            {
                listOfTempStatsModifier.Add(listOfAllStatsModifier[i]);
            }
        }

        for (int j = 0; j < numOfOptions; j++)
        {
            
            int index = Random.Range(0,listOfTempStatsModifier.Count);
            if (listOfTempStatsModifier.Count > 0)
            {
                listOfOptionalStatsModifier.Add(listOfTempStatsModifier[index]);
                listOfTempStatsModifier.Remove(listOfTempStatsModifier[index]);
            }
            else
            {
                listOfOptionalStatsModifier.Add(null);
            }
        }
    }


    public void LevelUp()
    {
        RandomlyAddToOptionalList(numOfOptions);

        if (listOfOptionalStatsModifier[0] != null || listOfTempStatsModifier.Count != 0)
        {
            gameManager.Pause();
            upgradePanel.SetActive(true);
            upgradePanelManager.DisplayButton();

        }
        

    }

}
