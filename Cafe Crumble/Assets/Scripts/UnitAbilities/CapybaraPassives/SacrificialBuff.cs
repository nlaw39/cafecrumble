using UnityEngine;

[CreateAssetMenu(fileName = "SacrificialBuff", menuName = "Scriptable Objects/SacrificialBuff")]
public class SacrificialBuff : PassiveAbility
{
    public override void OnTakeLead(BaseUnitScript self, BaseUnitScript target)
    {
        UnityEngine.Debug.Log(self.name + " gained power from its fallen ally!");
        self.UpdateAttackValue(2);
    }
}
