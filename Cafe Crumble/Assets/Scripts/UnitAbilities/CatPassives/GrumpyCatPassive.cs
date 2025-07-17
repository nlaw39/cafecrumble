using UnityEngine;

[CreateAssetMenu(fileName = "GrumpyCatPassive", menuName = "Scriptable Objects/GrumpyCatPassive")]
public class GrumpyCatPassive : PassiveAbility
{
    public override void OnTakeDamage(BaseUnitScript self, BaseUnitScript target)
    {
        UnityEngine.Debug.Log("Grumpy cat gained strength from being hit!");
        self.UpdateAttackValue(1);
        self.UpdateHealthValue(1);
    }
}
