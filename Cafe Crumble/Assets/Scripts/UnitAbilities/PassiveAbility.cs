using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class PassiveAbility : ScriptableObject
{
    public virtual void ActivateImmediately(BaseUnitScript self) { }
    public virtual void OnCombatStart(List<GameObject> AllyUnits) { }
    public virtual void OnAttack(BaseUnitScript self, BaseUnitScript target) { }
    public virtual void OnTakeDamage(BaseUnitScript self, BaseUnitScript attacker) { }
    public virtual void OnTakeLead(BaseUnitScript self, BaseUnitScript target) { }
    public virtual void OnDeath(BaseUnitScript self) { }
    public virtual void OnCombatEnd(BaseUnitScript self) { }
}
