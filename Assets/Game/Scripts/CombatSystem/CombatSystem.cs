using UnityEngine;

public static class CombatSystem
{
    public static float CalculateDamage(CharacterStats attacker, CharacterStats defender)
    {
        float effectiveArmor = Mathf.Max(defender.armor - attacker.armorPen, 0);
        float armorMultiplier = 100f / (100f + effectiveArmor);

        float damage = attacker.attackDamage * armorMultiplier;

        if (Random.value < attacker.critRate)
        {
            damage *= attacker.critDamage;
            Debug.Log("Chí mạng!");
        }

        return damage;
    }
}
