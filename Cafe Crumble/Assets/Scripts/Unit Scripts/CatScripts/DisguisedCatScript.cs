using UnityEngine;

public class DisguisedCatScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 1;
        baseAttackDamage = 3;

        healthGrowth = 1;
        attackGrowth = 2;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/DisguisedCatPassive");
        AddPassive(Instantiate(passive));

        base.Start();
    }
}
