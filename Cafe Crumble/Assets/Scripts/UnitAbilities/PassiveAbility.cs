using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class PassiveAbility : ScriptableObject
{
    public abstract void OnCombatStart(List<GameObject> AllyUnits);
    public abstract void OnAttack(BaseUnitScript self, BaseUnitScript target);
    public abstract void OnTakeDamage(BaseUnitScript self, BaseUnitScript attacker);
    public abstract void OnTakeLead(BaseUnitScript self, BaseUnitScript target)
}
