using UnityEngine;

[CreateAssetMenu(fileName = "PricklyHedgehogPassive", menuName = "Scriptable Objects/PricklyHedgehogPassive")]
public class PricklyHedgehogPassive : PassiveAbility
{
    public override void ActivateImmediately(BaseUnitScript self)
    {
        UnityEngine.Debug.Log("Updating " + self.name + "'s attack value to be equal to its health.");
        self.currentAttackDamage = self.currentHealthPoints;
        self.UpdateUIText();
    }

    public override void OnTakeDamage(BaseUnitScript self, BaseUnitScript attacker) 
    {
        UnityEngine.Debug.Log("Updating " + self.name + "'s attack value to be equal to its health.");
        self.currentAttackDamage = self.currentHealthPoints;
        self.UpdateUIText();
    }
}
