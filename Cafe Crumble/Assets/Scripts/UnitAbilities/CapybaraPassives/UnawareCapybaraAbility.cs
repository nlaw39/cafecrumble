using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnawareCapybara", menuName = "Scriptable Objects/UnawareCapybara")]
public class UnawareCapybaraAbility : PassiveAbility
{
    public override void OnTakeLead(BaseUnitScript self, BaseUnitScript target)
    {
        UnityEngine.Debug.Log(self.name + " has reset all positive stat changes on " + target.name);
        if (target.currentHealthPoints > target.baseHealthPoints)
        {
            target.currentHealthPoints = target.baseHealthPoints;
        }
        
        if (target.currentAttackDamage > target.baseAttackDamage)
        {
            target.currentAttackDamage = target.baseAttackDamage;
        }
        
    }
}
