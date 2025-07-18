using UnityEngine;

[CreateAssetMenu(fileName = "DisguisedCatPassive", menuName = "Scriptable Objects/DisguisedCatPassive")]
public class DisguisedCatPassive : PassiveAbility
{
    private bool disguiseReady;
    public override void OnTakeLead(BaseUnitScript self, BaseUnitScript target)
    {
        UnityEngine.Debug.Log(self.name + " readied its disguise!");
        self.isShielded = true;
    }
}
