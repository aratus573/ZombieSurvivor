using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Upgrade's name", menuName = "New Upgrade")]
public class StatsModifier : ScriptableObject
{
    public string Name;
    public string description;
    public Sprite icon;
    public int applyCount = 0;
    public int maxApplyCount = 5;
    public bool isLevelMax = false;

    [SerializeField]
    // targetAtt * (1 + (modifier1 + modifier2 + modifier3))
    public float modifier1 = 1f;
    public float modifier2 = 1f;
    
    // attributes to be modified, can be set in inspector
    public enum Attributes
    {
        none,
        MaxHealth,
        MoveSpeed,
        AttackDamage,
        MutationMultiplier,
        ExpMultiplier,
        DamageReduction,
        HealRate,
        BowDamage,
    }

    public Attributes targetAtt1 = Attributes.none;
    public Attributes targetAtt2 = Attributes.none;

}
