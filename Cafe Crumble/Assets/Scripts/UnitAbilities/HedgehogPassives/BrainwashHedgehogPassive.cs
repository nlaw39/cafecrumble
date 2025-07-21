using UnityEngine;

[CreateAssetMenu(fileName = "BrainwashHedgehogPassive", menuName = "Scriptable Objects/BrainwashHedgehogPassive")]
public class BrainwashHedgehogPassive : PassiveAbility
{
    public override void OnTakeLead(BaseUnitScript self, BaseUnitScript target)
    {
        UnityEngine.Debug.Log(self.name + " gained power from its fallen ally!");
        int storeTargetHP = target.currentHealthPoints;
        target.currentHealthPoints = target.currentAttackDamage;
        target.currentAttackDamage = storeTargetHP;
        target.UpdateUIText();
    }
}
