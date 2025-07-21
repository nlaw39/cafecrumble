using UnityEngine;

public class DressedDogScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 2;
        baseAttackDamage = 1;

        healthGrowth = 2;
        attackGrowth = 0;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/DressedUpDogPassive");
        AddPassive(Instantiate(passive));

        base.Start();
    }
}
