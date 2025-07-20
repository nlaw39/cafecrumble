using UnityEngine;

[CreateAssetMenu(fileName = "LazyCapybaraPassive", menuName = "Scriptable Objects/LazyCapybaraPassive")]
public class LazyCapybaraPassive : PassiveAbility
{
    public override void OnTakeDamage(BaseUnitScript self, BaseUnitScript target)
    {
        UnityEngine.Debug.Log(self.name + " gained health from being hit!");
        self.UpdateHealthValue(1);
    }
}
