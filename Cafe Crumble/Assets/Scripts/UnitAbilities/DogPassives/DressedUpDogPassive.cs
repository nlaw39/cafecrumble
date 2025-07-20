using UnityEngine;

[CreateAssetMenu(fileName = "DressedUpDogPassive", menuName = "Scriptable Objects/DressedUpDogPassive")]
public class DressedUpDogPassive : PassiveAbility
{
    public override void ActivateImmediately(BaseUnitScript self)
    {
        self.doubledStatChanges = true;
    }
}
