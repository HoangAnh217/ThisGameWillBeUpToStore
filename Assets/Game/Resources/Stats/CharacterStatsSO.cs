using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats/CharacterStats")]
public class CharacterStatsSO : ScriptableObject
{
    public float maxHP = 1000;
    public float attackDamage = 100;
    public float armor = 200;
    public float armorPenetration = 50;
    public float critRate = 0.2f;         // 20%
    public float critDamage = 1.5f;       // 150%
    public float attackSpeed = 1.2f;      // đòn/giây
}
