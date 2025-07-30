using UnityEngine;

[CreateAssetMenu(fileName = "SuperfanHedgehogPassive", menuName = "Scriptable Objects/SuperfanHedgehogPassive")]
public class SuperfanHedgehogPassive : PassiveAbility
{
    public override void OnAttack(BaseUnitScript self, BaseUnitScript target)
    {
        // what I really wanted to do here was allow the superfan to hit first in combat, but its too hard
        UnityEngine.Debug.Log(self.name + " gained strength from attacking!");
        self.UpdateAttackValue(2);
    }
}
