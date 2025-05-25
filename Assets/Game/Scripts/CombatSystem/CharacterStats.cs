using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStatsSO statsTemplate;

    [HideInInspector] public float currentHP;

    // Local copy để không chỉnh sửa ScriptableObject gốc
    public float attackDamage;
    public float armor;
    public float armorPen;
    public float critRate;
    public float critDamage;
    public float attackSpeed;

    void Start()
    {
        InitializeStats();
    }

    void InitializeStats()
    {
        currentHP = statsTemplate.maxHP;
        attackDamage = statsTemplate.attackDamage;
        armor = statsTemplate.armor;
        armorPen = statsTemplate.armorPenetration;
        critRate = statsTemplate.critRate;
        critDamage = statsTemplate.critDamage;
        attackSpeed = statsTemplate.attackSpeed;
    }
}
